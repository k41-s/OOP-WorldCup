using DataLayer.Models.Match;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace WorldCupStatsViewer.Views
{
    /// <summary>
    /// Interaction logic for PlayerOverviewWindow.xaml
    /// </summary>
    public partial class PlayerOverviewWindow : Window
    {
        public PlayerOverviewWindow(MatchPlayer player, string imagePath, int goals, int yellowCards)
        {
            InitializeComponent();

            // Set image
            imgPlayer.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

            // Set text fields
            tbName.Text = player.Name;
            tbNumber.Text = $"Shirt Number: {player.ShirtNumber}";
            tbPosition.Text = $"Position: {player.Position}";
            tbCaptain.Text = $"Captain: {(player.Captain ? "True" : "False")}";
            tbGoals.Text = $"Goals this match: {goals}";
            tbYellowCards.Text = $"Yellow cards this match: {yellowCards}";
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
