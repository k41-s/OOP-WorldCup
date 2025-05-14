using DataLayer.Models.Match;
using Utilities;

namespace WorldCupManager
{
    public partial class PlayerControl : UserControl
    {// error mapping bitmap to byte or something fucking retarded
        public readonly MatchPlayer _player;
        private Stream? _stream;

        public PlayerControl(MatchPlayer player)
        {
            InitializeComponent();
            _player = player;
            lblName.Text = _player.ToString();

            RegisterMouseDownRecursive(this);
        }

        private void LoadPlayerImage()
        {
            _stream = new MemoryStream(Properties.Resources.No_Player_Img);
            Image defaultImg = Image.FromStream(_stream);

            try
            {
                string imagePath = Utility.GetPlayerImagePath(_player.Name);

                if (File.Exists(imagePath))
                {
                    pbPlayerImg.Image = Image.FromFile(imagePath);
                }
                else
                {
                    pbPlayerImg.Image = defaultImg;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load img for {_player.Name}: {ex.Message}", "Image Error");
                pbPlayerImg.Image = defaultImg;
            }
        }

        // Make sure each child control has event hooked up
        private void RegisterMouseDownRecursive(Control control)
        {
            control.MouseDown += PlayerControl_MouseDown;

            foreach (Control child in control.Controls)
            {
                RegisterMouseDownRecursive(child);
            }
        }

        private void PlayerControl_MouseDown(object sender, MouseEventArgs e)
        {
            Control clickedControl = GetChildAtPoint(e.Location);
            if (clickedControl == pbPlayerImg)
                return;

            if (e.Button == MouseButtons.Left)
            {
                this.DoDragDrop(this, DragDropEffects.Move);
            }

        }

        private void pbPlayerImg_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Utility.SetPlayerImage(ofd.FileName, _player.Name);
                    LoadPlayerImage(); // Reload image
                }
            }
        }

        private void PlayerControl_Load(object sender, EventArgs e)
        {
            LoadPlayerImage();
        }
    }
}
