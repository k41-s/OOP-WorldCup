using Utilities;
using WorldCupManager.Models;

namespace WorldCupManager
{
    public partial class PlayerStatsControl : UserControl
    {
        private readonly PlayerRankingStats _playerStats;
        private Stream? _stream;
        public PlayerStatsControl(PlayerRankingStats stats)
        {
            InitializeComponent();

            _playerStats = stats;
            lblPlayerStats.Text = _playerStats.ToString();
        }

        private void LoadPlayerImage()
        {
            _stream = new MemoryStream(Properties.Resources.No_Player_Img);
            Image defaultImg = Image.FromStream(_stream);

            try
            {
                if (!string.IsNullOrEmpty(_playerStats.ImagePath) && File.Exists(_playerStats.ImagePath))
                    pbPlayerImg.Image = Image.FromFile(_playerStats.ImagePath);
                else
                    pbPlayerImg.Image = defaultImg;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load image for {_playerStats.Player.Name}: {ex.Message}");
                pbPlayerImg.Image = defaultImg;
            }
        }

        private void PlayerStatsControl_Load(object sender, EventArgs e)
        {
            LoadPlayerImage();
        }

        private void pbPlayerImg_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Utility.SetPlayerImage(ofd.FileName, _playerStats.Player.Name);
                    LoadPlayerImage(); // Reload image and control
                }
            }
        }
    }
}
