using DataLayer.Enums;
using DataLayer.Models.Match;
using DataLayer.Services;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Utilities;
using WorldCupStatsViewer.Controls;

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

            // Apply football pitch background
            pitchImg.Source = new BitmapImage(new Uri(Utility.footballPitchImgPath));
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

                // Reset the field
                canvasPlayers.Children.Clear();

                tbFavTeam.Text = "Favorite Team";
                tbOppTeam.Text = "Opponent Team";
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

                tbFavTeam.Text = Utility.GetTeamByFifaCode(_selectedTeamFifaCode, _allMatches)?.Country;
                tbOppTeam.Text = opponentTeam.Country;

                // Populate the pitch with players
                bool isFavoriteHome = match.HomeTeam.FifaCode == _selectedTeamFifaCode;

                IList<MatchPlayer> favoritePlayers = isFavoriteHome ? match.HomeTeamStatistics.StartingEleven : match.AwayTeamStatistics.StartingEleven;
                IList<MatchPlayer> opponentPlayers = isFavoriteHome ? match.AwayTeamStatistics.StartingEleven : match.HomeTeamStatistics.StartingEleven;

                DisplayPlayersOnPitch(favoritePlayers, true);
                DisplayPlayersOnPitch(opponentPlayers, false);
            }
        }

        // Mess with locations, left or right etc.
        private void DisplayPlayersOnPitch(IList<MatchPlayer> players, bool isFavTeam)
        {
            if (isFavTeam)
                canvasPlayers.Children.Clear();

            double canvasWidth = canvasPlayers.ActualWidth;
            double canvasHeight = canvasPlayers.ActualHeight;

            if(canvasWidth == 0 || canvasHeight== 0)
            {
                // (_, _) discards the Loaded delegate necessary params
                canvasPlayers.Loaded += (_, _) => DisplayPlayersOnPitch(players, isFavTeam);
                return;
            }

            IDictionary<Position, List<MatchPlayer>> grouped = players
                .GroupBy(p => p.Position)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Horizontal position on pitch (left for favorite team, right for opponent)
            IDictionary<Position, double> positionX = isFavTeam
                ? new Dictionary<Position, double> // Left half (0.0–0.5)
                {
                    [Position.Goalie] = 0.1,
                    [Position.Defender] = 0.2,
                    [Position.Midfield] = 0.3,
                    [Position.Forward] = 0.4
                }
                : new Dictionary<Position, double> // Right half (0.5–1.0)
                {
                    [Position.Goalie] = 0.9,
                    [Position.Defender] = 0.8,
                    [Position.Midfield] = 0.7,
                    [Position.Forward] = 0.6
                };

            foreach (var position in grouped.Keys)
            {
                List<MatchPlayer> playersInPosition = grouped[position];
                double x = canvasWidth * positionX[position];

                for (int i = 0; i < playersInPosition.Count; i++)
                {
                    double spacing = canvasHeight / (playersInPosition.Count + 1);
                    double y = spacing * (i + 1); // evenly distribute vertically

                    Control control = GetPlayerPitchControl(playersInPosition[i]);

                    Canvas.SetLeft(control, x - control.Width / 2);
                    Canvas.SetTop(control, y - control.Height / 2);

                    canvasPlayers.Children.Add(control);
                }
            }
        }

        private PlayerOnFieldControl GetPlayerPitchControl(MatchPlayer matchPlayer)
        {
            string name = matchPlayer.Name;
            long shirtNumber = matchPlayer.ShirtNumber;
            string imgPath = Utility.GetPlayerImagePath(name);

            return new PlayerOnFieldControl(
                name,
                shirtNumber,
                imgPath);
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
