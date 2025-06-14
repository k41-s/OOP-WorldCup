using DataLayer.Enums;
using DataLayer.Models.Match;
using DataLayer.Services;
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
        private MatchData? _currentMatch;
        private IDictionary<string, string> _userSettings;

        // Resolution
        private string? _displayMode;

        private IList<MatchPlayer>? _lastFavPlayers;
        private IList<MatchPlayer>? _lastOppPlayers;

        public NationalTeamView(IDataService dataService)
        {
            InitializeComponent();
            _dataService = dataService;

            _userSettings = Utility.LoadUserSettings();
            _category = CategoryHelper.GetCategory(_userSettings["Category"]);
            tbCategory.Text = CategoryHelper.GetCategoryAsString(_category);

            _displayMode = _userSettings["DisplayMode"];
        }

        private async void NationalTeamView_Loaded(object sender, RoutedEventArgs e)
        {
            Window loading = new LoadingWindow();
            loading.Show();
            loading.BringIntoView();

            try
            {
                ApplyDisplayMode();

                // Load favorite team from file
                try
                {
                    _selectedTeamFifaCode = Utility.LoadFavouriteTeamCode();
                }
                catch (Exception ex)
                {
                    // Fav team wont be defaulted, do nothing
                }

                _allMatches = await _dataService.GetMatchDataAsync(_category);

                _allTeams = _allMatches
                    .Select(m => m.HomeTeam)
                    .Concat(_allMatches.Select(m => m.AwayTeam))
                    .GroupBy(t => t.FifaCode) // Remove duplicates using FIFA code
                    .Select(g => g.First()) // Take first team with each unique code
                    .ToList();

                IList<MatchTeam> opponents = Utility.GetOpponents(_selectedTeamFifaCode, _allMatches);

                cbFavoriteTeam.ItemsSource = _allTeams;
                cbOpponentTeam.ItemsSource = opponents;

                // Auto select favorite team by matching FIFA code
                try
                {
                    int defaultIndex = _allTeams.ToList().FindIndex(item => item.ToString().EndsWith($"({_selectedTeamFifaCode})"));
                    if (defaultIndex != -1)
                    {
                        cbFavoriteTeam.SelectedIndex = defaultIndex;
                    }
                    else
                    {
                        cbFavoriteTeam.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No favorite team found, default team has been set: {ex.Message}");
                    cbFavoriteTeam.SelectedIndex = 0;
                }
                cbOpponentTeam.SelectedIndex = 0;

                CalcMatchResultAndDisplayPlayers();

                // Apply football pitch background
                pitchImg.Source = new BitmapImage(new Uri(Utility.footballPitchImgPath));
            }
            finally
            {
                loading.Close();
            }
        }

        private void ApplyDisplayMode()
        {
            // No need to handle cases of no stored displayMode, defaults are in code
            if (!string.IsNullOrEmpty(_displayMode))
            {
                if (_displayMode.Trim().ToLower() == "fullscreen")
                    this.WindowState = WindowState.Maximized;
                else
                {
                    string[] displaySettings = _displayMode.Split('x');

                    bool parseWidth = int.TryParse(displaySettings[0], out int width);
                    bool parseHeight = int.TryParse(displaySettings[1], out int height);

                    if (parseWidth && parseHeight)
                    {
                        this.Width = width;
                        this.Height = height;
                    }
                }
            }
        }

        private void ComboBoxes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_allMatches == null)
            {
                MessageBox.Show("Matches list is null");
                return;
            }

            // Update the Selected team and fifa code
            MatchTeam? selectedTeam = cbFavoriteTeam.SelectedItem as MatchTeam;
            if (selectedTeam == null)
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

                // Auto select top of list
                if (opponents.Any())
                    cbOpponentTeam.SelectedIndex = 0;

                CalcMatchResultAndDisplayPlayers();
            }

            // Only calculate result if both are selected and opponent is sender
            if (sender == cbOpponentTeam && cbOpponentTeam.SelectedItem != null)
            {
                CalcMatchResultAndDisplayPlayers();
            }
        }

        private void CalcMatchResultAndDisplayPlayers()
        {
            Window loading = new LoadingWindow();
            loading.Show();
            loading.BringIntoView();

            try
            {
                if (_allMatches == null)
                {
                    MessageBox.Show("Matches list is null");
                    return;
                }

                MatchTeam? opponentTeam = cbOpponentTeam.SelectedItem as MatchTeam;
                if (opponentTeam == null)
                {
                    tbMatchResult.Text = "No opponent selected.";
                    return;
                }
                string opponentCode = opponentTeam.FifaCode;

                _currentMatch = _allMatches.FirstOrDefault(m =>
                    (m.HomeTeam.FifaCode == _selectedTeamFifaCode && m.AwayTeam.FifaCode == opponentCode) ||
                    (m.HomeTeam.FifaCode == opponentCode && m.AwayTeam.FifaCode == _selectedTeamFifaCode)
                );

                if (_currentMatch == null)
                {
                    tbMatchResult.Text = "No match found.";
                    return;
                }

                int favoriteGoals = Utility.CalcGoalsForTeam(_currentMatch, _selectedTeamFifaCode);
                int opponentGoals = Utility.CalcGoalsForTeam(_currentMatch, opponentCode);

                tbMatchResult.Text = $"{favoriteGoals} : {opponentGoals}";

                tbFavTeam.Text = Utility.GetTeamByFifaCode(_selectedTeamFifaCode, _allMatches)?.Country;
                tbOppTeam.Text = opponentTeam.Country;

                // Populate the pitch with players
                bool isFavoriteHome = _currentMatch.HomeTeam.FifaCode == _selectedTeamFifaCode;

                IList<MatchPlayer>? favoritePlayers = isFavoriteHome ? _currentMatch.HomeTeamStatistics.StartingEleven : _currentMatch.AwayTeamStatistics.StartingEleven;
                IList<MatchPlayer>? opponentPlayers = isFavoriteHome ? _currentMatch.AwayTeamStatistics.StartingEleven : _currentMatch.HomeTeamStatistics.StartingEleven;

                if (favoritePlayers == null || opponentPlayers == null)
                {
                    MessageBox.Show("favorite players or opponent players are null");
                    return;
                }

                DisplayPlayersOnPitch(favoritePlayers, true);
                DisplayPlayersOnPitch(opponentPlayers, false);
            }
            finally
            {
                loading.Close();
            }
        }

        private void DisplayPlayersOnPitch(IList<MatchPlayer> players, bool isFavTeam)
        {
            // Cache for resize
            if (isFavTeam)
            {
                _lastFavPlayers = players;
                canvasPlayers.Children.Clear();
            }
            else
            {
                _lastOppPlayers = players;
            }

            double canvasWidth = canvasPlayers.ActualWidth;
            double canvasHeight = canvasPlayers.ActualHeight;

            // Try again if pitch not set yet
            if (canvasWidth == 0 || canvasHeight == 0)
            {
                // (_, _) discards the Loaded delegate necessary params
                canvasPlayers.Loaded += (_, _) => DisplayPlayersOnPitch(players, isFavTeam);
                return;
            }

            // Group players into dictionary by position
            IDictionary<Position, List<MatchPlayer>> grouped = players
                .GroupBy(p => p.Position)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Horizontal position on pitch (left for favorite team, right for opponent)
            IDictionary<Position, double> xPositions = isFavTeam
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
                // Store all players who play in the current position
                IList<MatchPlayer> playersInPosition = grouped[position];

                // X coord of players in this position
                double x = canvasWidth * xPositions[position];

                // Assign location for each player in this position
                for (int i = 0; i < playersInPosition.Count; i++)
                {
                    // Make sure players don't overlap
                    double yRatio = ((i + 1.0) / (playersInPosition.Count + 1));
                    double y = canvasHeight * yRatio;

                    Control? control = GetPlayerPitchControl(playersInPosition[i]);

                    if (control == null)
                        return;

                    // Put player control in appropriate position
                    Canvas.SetLeft(control, x - control.Width / 2);
                    Canvas.SetTop(control, y - control.Height / 2);

                    canvasPlayers.Children.Add(control);
                }
            }
        }

        private PlayerOnFieldControl? GetPlayerPitchControl(MatchPlayer matchPlayer)
        {
            if (_currentMatch == null)
            {
                MessageBox.Show("Current match invalid/null");
                return null;
            }
            return new PlayerOnFieldControl(matchPlayer, _currentMatch);
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

        private void canvasPlayers_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_lastFavPlayers != null)
                DisplayPlayersOnPitch(_lastFavPlayers, true);
            if (_lastOppPlayers != null)
                DisplayPlayersOnPitch(_lastOppPlayers, false);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Window window = new ExitConfirmationWindow();
            bool? result = window.ShowDialog();

            if (!result.HasValue)
                return;

            // If user confirms they want to exit
            if (result.Value)
            {
                this.Close();
                return;
            }

            // Do nothing if they do not confirm
            return;
        }

        private async void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            Window window = new InitialSettingsView();

            bool? result = window.ShowDialog();

            if (!result.HasValue)
                return;

            // If user changed settings
            if (result.Value)
            {
                await ClearWindowAndResetValuesAsync();

                return;
            }

            // If nothing changed
            return;
        }

        private async Task ClearWindowAndResetValuesAsync()
        {
            Window loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            loadingWindow.BringIntoView();
            try
            {
                // Reload user settings
                _userSettings = Utility.LoadUserSettings();
                _category = CategoryHelper.GetCategory(_userSettings["Category"]);
                tbCategory.Text = CategoryHelper.GetCategoryAsString(_category);

                _displayMode = _userSettings["DisplayMode"];

                ApplyDisplayMode();

                // Reload team code
                try
                {
                    _selectedTeamFifaCode = Utility.LoadFavouriteTeamCode();
                }
                catch (Exception ex)
                {
                    // Fav team wont be defaulted, do nothing
                }

                // 3. Clear controls
                cbFavoriteTeam.ItemsSource = null;
                cbOpponentTeam.ItemsSource = null;
                canvasPlayers.Children.Clear();
                tbMatchResult.Text = "";
                tbFavTeam.Text = "Favorite Team";
                tbOppTeam.Text = "Opponent Team";

                // 4. Reset cached match/player data
                _lastFavPlayers = null;
                _lastOppPlayers = null;
                _currentMatch = null;

                // 5. Reload match data and teams
                try
                {
                    _allMatches = await _dataService.GetMatchDataAsync(_category);

                    _allTeams = _allMatches
                        .Select(m => m.HomeTeam)
                        .Concat(_allMatches.Select(m => m.AwayTeam))
                        .GroupBy(t => t.FifaCode)
                        .Select(g => g.First())
                        .ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading matches: {ex.Message}");
                    return;
                }

                // Check if the selected favorite team is valid for the selected category
                bool favTeamExistsInCategory = _allTeams.Any(t => t.FifaCode == _selectedTeamFifaCode);
                if (!favTeamExistsInCategory)
                {
                    MessageBox.Show("Your favorite team has not played in the selected championship.\nPlease choose a different category.");
                    tbMatchResult.Text = "Select a favorite team above";
                }

                IList<MatchTeam> opponents = Utility.GetOpponents(_selectedTeamFifaCode, _allMatches);

                cbFavoriteTeam.ItemsSource = _allTeams;
                cbOpponentTeam.ItemsSource = opponents;

                // Auto-select favorite team again
                try
                {
                    int defaultIndex = _allTeams.ToList().FindIndex(item => item.ToString().EndsWith($"({_selectedTeamFifaCode})"));
                    if (defaultIndex != -1)
                    {
                        cbFavoriteTeam.SelectedIndex = defaultIndex;
                    }
                    else
                    {
                        cbFavoriteTeam.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No favorite team found, default team has been set: {ex.Message}");
                    cbFavoriteTeam.SelectedIndex = 0;
                }

                // 7. Reload pitch background if needed
                pitchImg.Source = new BitmapImage(new Uri(Utility.footballPitchImgPath));
            }
            finally
            {
                loadingWindow.Close();
            }
        }
    }
}
