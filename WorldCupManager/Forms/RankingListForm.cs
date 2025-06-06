using DataLayer.Enums;
using DataLayer.Models.Match;
using DataLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using Utilities;
using WorldCupManager.Models;
using WorldCupManager.UserControls;

namespace WorldCupManager
{
    public partial class RankingListForm : Form
    {
        // Init necessary variables
        private IDataService _dataService;

        private IList<PlayerRankingStats> _playerStats;
        private IList<MatchRankingStats> _matchStats;

        private IList<MatchData> _matches;
        private IList<MatchPlayer> _players;

        private string? _selectedCountryFifaCode;
        private MatchTeam? _selectedCountry;
        private string _teamName;

        private Category _category;

        // Inject Service Provider and IDataService through constructor
        public RankingListForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _dataService = serviceProvider.GetRequiredService<IDataService>();

            IDictionary<string, string> userSettings = Utility.LoadUserSettings();
            _category = CategoryHelper.GetCategory(userSettings["Category"]);
        }

        private async void RankingListForm_Load(object sender, EventArgs e)
        {
            using LoadingForm lf = new();
            lf.Show();
            lf.Refresh();

            try
            {
                _selectedCountryFifaCode = Utility.LoadFavouriteTeamCode();

                if (_selectedCountryFifaCode != null)
                {
                    _matches = await _dataService.GetMatchDataByCountryAsync(_category, _selectedCountryFifaCode);

                    _selectedCountry = Utility.GetTeamByFifaCode(_selectedCountryFifaCode, _matches);
                }

                if (_selectedCountry != null)
                    _teamName = _selectedCountry.Country;

                lblCategory.Text = CategoryHelper.GetCategoryAsString(_category);
                lblTeamName.Text = _teamName;

                _players = await GetPlayers();

                _playerStats = new List<PlayerRankingStats>();
                _matchStats = new List<MatchRankingStats>();

                FillPlayerRankings();
                FillMatchRankings();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading form: {ex.Message}");

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
        }

        // Work on Match Rankings
        private void FillMatchRankings()
        {
            foreach (var match in _matches)
            {
                try
                {
                    MatchRankingStats? matchStat = BuildMatchStats(match);

                    if (matchStat != null)
                        _matchStats.Add(matchStat);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding match {match}: {ex.Message}");
                }
            }

            _matchStats = _matchStats.Order().ToList();

            // Add matchStats to FlowLayoutPanel
            foreach (var stats in _matchStats)
            {
                Control control = new MatchStatsControl(stats);
                flpMatches.Controls.Add(control);
            }
        }

        private void FillPlayerRankings()
        {
            // Load up list of playerStats
            foreach (var player in _players)
            {
                // Nested try catch for more accurate error checking
                try
                {
                    PlayerRankingStats? playerStat = BuildPlayerStats(player);

                    if (playerStat != null)
                        _playerStats.Add(playerStat);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding player {player}: {ex.Message}");
                }
            }

            _playerStats = OrderPlayerStats(_playerStats);

            // Add playerStats to FlowLayoutPanel
            foreach (var stats in _playerStats)
            {
                Control control = new PlayerStatsControl(stats);
                flpPlayers.Controls.Add(control);
            }
        }

        private static IList<PlayerRankingStats> OrderPlayerStats(IList<PlayerRankingStats> list)
        {
            return list
                .OrderByDescending(ps => ps.Goals)
                .ThenBy(ps => ps.YellowCards)
                .ToList();
        }

        private MatchRankingStats? BuildMatchStats(MatchData match)
        {
            if (match == null)
                return null;

            return new MatchRankingStats
            {
                FifaId = match.FifaId,
                Location = match.Location,
                Visitors = match.Attendance,
                HomeTeam = match.HomeTeam,
                AwayTeam = match.AwayTeam
            };
        }

        private PlayerRankingStats? BuildPlayerStats(MatchPlayer player)
        {
            if (player == null || _selectedCountry == null || _matches == null || _matches.Count == 0)
                return null;

            return new PlayerRankingStats
            {
                Player = player,
                Goals = Utility.CalcGoalsForPlayer(player, _matches, _teamName),
                YellowCards = Utility.CalcYellowCardsForPlayer(player, _matches, _teamName),
                Appearances = Utility.CalcAppearancesForPlayer(player, _matches, _teamName),
                ImagePath = Utility.GetPlayerImagePath(player.Name)
            };
        }

        private async Task<List<MatchPlayer>> GetPlayers()
        {
            if (string.IsNullOrEmpty(_selectedCountryFifaCode))
                throw new InvalidOperationException("No favourite team selected");

            MatchData? firstMatch =
                _matches.FirstOrDefault(m => m.HomeTeam.FifaCode == _selectedCountryFifaCode
                || m.AwayTeam.FifaCode == _selectedCountryFifaCode);

            if (firstMatch == null)
                return new List<MatchPlayer>();

            bool isHomeTeam = firstMatch.HomeTeam.FifaCode == _selectedCountryFifaCode;

            TeamStatistics stats = isHomeTeam
                ? firstMatch.HomeTeamStatistics
                : firstMatch.AwayTeamStatistics;

            return stats.StartingEleven
                .Union(stats.Substitutes)
                .ToList();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitForm();
        }

        private async void btnSettings_Click(object sender, EventArgs e)
        {
            // Show settings form
            using SettingsForm settingsForm = new();
            var result = settingsForm.ShowDialog();

            if(result == DialogResult.OK)
            {
                // Reload ranking list is settings saved
                await ClearWindowAndResetValuesAsync();
            }
        }

        private async Task ClearWindowAndResetValuesAsync()
        {
            using (LoadingForm lf = new())
            {
                lf.Show();
                lf.Refresh();

                try
                {
                    // Clear UI controls
                    flpPlayers.Controls.Clear();
                    flpMatches.Controls.Clear();

                    // Reset variables
                    _playerStats = new List<PlayerRankingStats>();
                    _matchStats = new List<MatchRankingStats>();
                    _matches = new List<MatchData>();
                    _players = new List<MatchPlayer>();
                    _selectedCountryFifaCode = null;
                    _selectedCountry = null;
                    _teamName = string.Empty;

                    // Reload settings
                    IDictionary<string, string> userSettings = Utility.LoadUserSettings();
                    Category newCat = CategoryHelper.GetCategory(userSettings["Category"]);

                    // Only reload if category has changed
                    if (newCat != _category)
                    {
                        _category = newCat;
                    }
                    lblCategory.Text = CategoryHelper.GetCategoryAsString(_category);

                    // Reload team selection and match data
                    _selectedCountryFifaCode = Utility.LoadFavouriteTeamCode();

                    // Validate the team code exists in this category
                    if (!string.IsNullOrEmpty(_selectedCountryFifaCode))
                    {
                        try
                        {
                            _matches = await _dataService.GetMatchDataByCountryAsync(_category, _selectedCountryFifaCode);
                            _selectedCountry = Utility.GetTeamByFifaCode(_selectedCountryFifaCode, _matches);
                        }
                        catch (Exception ex)
                        {
                            // Invalid team for new category
                            MessageBox.Show("Selected favorite team is not available in this category. Please select a new one.", "Invalid Team", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            lf.Close();

                            using FavouriteTeamForm form = new(_dataService);
                            form.BringToFront();

                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                // Since fav team changed, players should also change
                                using FavPlayersForm favPlayersForm = new(_dataService);
                                favPlayersForm.ShowDialog();

                                // Retry loading with new favorite team
                                await ClearWindowAndResetValuesAsync();
                            }

                            return;
                        }
                    }


                    if (_selectedCountry != null)
                        _teamName = _selectedCountry.Country;
                    lblTeamName.Text = _teamName;

                    // Reload players
                    _players = await GetPlayers();

                    // Refill stats and update UI
                    FillPlayerRankings();
                    FillMatchRankings();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to reload data: {ex.Message}");
                }
            };
        }

        private void ExitForm()
        {
            using ExitConfirmationForm form = new();
            DialogResult dialogResult = form.ShowDialog();

            // Ok for confirmation of exiting
            if (dialogResult == DialogResult.OK)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else if (dialogResult == DialogResult.Cancel) // Decided not to leave application
                return;
        }

        private int currentPrintLine;
        private IList<string> linesToPrint;
        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font printFont = this.Font ?? new Font("Arial", 12);

            int lineHeight = (int)Font.GetHeight(e.Graphics) + 4;
            float y = e.MarginBounds.Top;

            while (currentPrintLine < linesToPrint.Count)
            {
                string line = linesToPrint[currentPrintLine];
                e.Graphics.DrawString(line, Font, Brushes.Black, e.MarginBounds.Left, y);
                y += lineHeight;

                if (y + lineHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                ++currentPrintLine;
            }

            e.HasMorePages = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            currentPrintLine = 0;
            linesToPrint = BuildPrintableRankingLines();

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private IList<string> BuildPrintableRankingLines()
        {
            IList<string> lines = new List<string>();
            lines.Add("Player Rankings");
            lines.Add("----------------------");

            foreach (var player in _playerStats)
                lines.Add(player.FormatForPrinting());

            lines.Add("");

            lines.Add("Match Rankings");
            lines.Add("----------------------");

            foreach (var match in _matchStats)
                lines.Add(match.FormatForPrinting());

            return lines;
        }

        private void printDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // Ensure form shows up after printing
            this.Activate();

            // Display success message
            MessageBox.Show("Printed successfully!");
        }
    }
}
