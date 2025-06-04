using DataLayer.Models.Match;
using DataLayer.Services;
using System.Windows;
using Utilities;

namespace WorldCupStatsViewer.Views
{
    /// <summary>
    /// Interaction logic for TeamInfoWindow.xaml
    /// </summary>
    public partial class TeamInfoWindow : Window
    {
        private readonly IList<MatchData> _allMatches;
        private readonly string _selectedTeamCode;
        public TeamInfoWindow(IList<MatchData> allMatches, string fifaCode)
        {
            InitializeComponent();

            _allMatches = allMatches;
            _selectedTeamCode = fifaCode;

            LoadTeamStats();
        }

        private void LoadTeamStats()
        {
            IList<MatchData> teamMatches = _allMatches
            .Where(m => m.HomeTeam.FifaCode == _selectedTeamCode || 
                m.AwayTeam.FifaCode == _selectedTeamCode)
            .ToList();

            // Init stats variables
            int wins = 0, losses = 0, draws = 0, goalsScored = 0, goalsConceded = 0;

            foreach (var match in teamMatches)
            {
                bool isHomeTeam = match.HomeTeam.FifaCode == _selectedTeamCode;

                string? teamName = isHomeTeam ? match.HomeTeamCountry : match.AwayTeamCountry;
                string? opponentName = isHomeTeam ? match.AwayTeamCountry : match.HomeTeamCountry;
                string? oppCode = Utility.GetFifaCodeByTeamName(opponentName, _allMatches);

                if (teamName == null || opponentName == null || oppCode == null)
                    return;

                int goalsFor = Utility.CalcGoalsForTeam(match, _selectedTeamCode);
                int goalsAgainst = Utility.CalcGoalsForTeam(match, oppCode);

                goalsScored += goalsFor;
                goalsConceded += goalsAgainst;

                if (goalsFor > goalsAgainst) wins++;
                else if (goalsFor < goalsAgainst) losses++;
                else draws++;

                tbTeamName.Text = $"Team: {teamName}";
                tbTotalMatches.Text = $"Matches Played: {teamMatches.Count}";
                tbWins.Text = $"Wins: {wins}";
                tbLosses.Text = $"Losses: {losses}";
                tbDraws.Text = $"Draws: {draws}";
                tbGoalsScored.Text = $"Goals Scored: {goalsScored}";
                tbGoalsConceded.Text = $"Goals Conceded: {goalsConceded}";
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
