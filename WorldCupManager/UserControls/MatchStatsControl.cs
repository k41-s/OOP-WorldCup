using WorldCupManager.Models;

namespace WorldCupManager.UserControls
{
    public partial class MatchStatsControl : UserControl
    {
        private readonly MatchRankingStats _playerStats;
        public MatchStatsControl(MatchRankingStats playerStats)
        {
            InitializeComponent();
            _playerStats = playerStats;
        }

        private void MatchStatsControl_Load(object sender, EventArgs e)
        {
            lblMatchStats.Text = _playerStats.ToString();
        }
    }
}
