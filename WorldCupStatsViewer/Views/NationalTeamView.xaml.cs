using DataLayer.Enums;
using DataLayer.Models.Match;
using DataLayer.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Utilities;

namespace WorldCupStatsViewer.Views
{
    /// <summary>
    /// Interaction logic for FavoriteTeamView.xaml
    /// </summary>
    public partial class NationalTeamView : Window
    {
        private readonly IDataService _dataService;
        private IList<MatchData>? _allMatches;
        private string? _selectedTeamFifaCode;
        private Category _category;
        private IList<MatchTeam>? _allTeams;

        public NationalTeamView(IDataService dataService)
        {
            InitializeComponent();
            _dataService = dataService;

            IDictionary<string, string> userSettings = Utility.LoadUserSettings();
            _category = CategoryHelper.GetCategory(userSettings["Category"]);

            Loaded += NationalTeamView_Loaded;
        }

        private async void NationalTeamView_Loaded(object sender, RoutedEventArgs e)
        {
            // Load favorite team from file
            try
            {
                _selectedTeamFifaCode = Utility.LoadFavouriteTeamCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading favourite team: {ex.Message}");
                return;
            }

            _allMatches = await _dataService.GetMatchDataAsync(_category);

            _allTeams = _allMatches
                .Select(m => m.HomeTeam)
                .Concat(_allMatches.Select(m => m.AwayTeam))
                .GroupBy(t => t.FifaCode) // Step 2: Remove duplicates using FIFA code
                .Select(g => g.First()) // Take first team with each unique code
                .ToList();

            IList<MatchTeam> opponents = Utility.GetOpponents(_selectedTeamFifaCode, _allMatches);

            cbFavoriteTeam.ItemsSource = _allTeams;
            cbOpponentTeam.ItemsSource = opponents;

            // Auto select favorite team by matching FIFA code
            int defaultIndex = _allTeams.ToList().FindIndex(item => item.ToString().EndsWith($"({_selectedTeamFifaCode})"));
            if (defaultIndex != -1)
            {
                cbFavoriteTeam.SelectedIndex = defaultIndex;
            }
        }

        private void ComboBoxes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(_allMatches == null)
            {
                MessageBox.Show("Matches list is null");
                return;
            }

            // Update the Selected team and fifa code
            MatchTeam? selectedTeam = cbFavoriteTeam.SelectedItem as MatchTeam;
            if(selectedTeam == null)
            {
                tbMatchResult.Text = "No team selected.";
                return;
            }
            _selectedTeamFifaCode = selectedTeam.FifaCode;

            // Double safety in case of any errors
            if (_selectedTeamFifaCode == null)
            {
                tbMatchResult.Text = "No team selected.";
                return;
            }

            // Update opponents, but only if the favorite team selection changed
            if (sender == cbFavoriteTeam)
            {
                IList<MatchTeam> opponents = Utility.GetOpponents(_selectedTeamFifaCode, _allMatches);

                cbOpponentTeam.ItemsSource = opponents;
                cbOpponentTeam.SelectedItem = null;

                tbMatchResult.Text = "";
                return; // Don’t continue — we wait until both boxes are selected
            }

            // Only calculate result if both are selected and opponent is sender
            if (sender == cbOpponentTeam && cbOpponentTeam.SelectedItem != null)
            {
                MatchTeam? opponentTeam = cbOpponentTeam.SelectedItem as MatchTeam;
                if (opponentTeam == null)
                {
                    tbMatchResult.Text = "No opponent selected.";
                    return;
                }
                string opponentCode = opponentTeam.FifaCode;

                MatchData? match = _allMatches.FirstOrDefault(m =>
                    (m.HomeTeam.FifaCode == _selectedTeamFifaCode && m.AwayTeam.FifaCode == opponentCode) ||
                    (m.HomeTeam.FifaCode == opponentCode && m.AwayTeam.FifaCode == _selectedTeamFifaCode)
                );

                if (match == null)
                {
                    tbMatchResult.Text = "No match found.";
                    return;
                }

                int favoriteGoals = Utility.CalcGoalsForTeam(match, _selectedTeamFifaCode);
                int opponentGoals = Utility.CalcGoalsForTeam(match, opponentCode);

                tbMatchResult.Text = $"{favoriteGoals} : {opponentGoals}";
            }
        }

        private void OnFavoriteTeamInfoClick(object sender, RoutedEventArgs e)
        {
            if (cbFavoriteTeam == null)
                return;

            ShowTeamInfo(_selectedTeamFifaCode);
        }

        private void OnOpponentTeamInfoClick(object sender, RoutedEventArgs e)
        {
            if (cbOpponentTeam.SelectedItem == null)
                return;

            MatchTeam? opponentTeam = cbOpponentTeam.SelectedItem as MatchTeam;
            if (opponentTeam == null)
            {
                tbMatchResult.Text = "No opponent selected.";
                return;
            }
            string opponentCode = opponentTeam.FifaCode;

            ShowTeamInfo(opponentCode);
        }

        // Animate window fade in
        private void ShowTeamInfo(string? fifaCode)
        {
            if (string.IsNullOrEmpty(fifaCode) || _allMatches == null)
                return;

            Window window = new TeamInfoWindow(_allMatches, fifaCode);
            window.Opacity = 0;
            window.Show();

            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
            window.BeginAnimation(Window.OpacityProperty, fadeIn);
        }
    }
}
