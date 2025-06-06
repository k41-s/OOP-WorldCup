using DataLayer.Models.Match;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Utilities;
using WorldCupStatsViewer.Views;

namespace WorldCupStatsViewer.Controls
{
    /// <summary>
    /// Interaction logic for PlayerOnFieldControl.xaml
    /// </summary>
    public partial class PlayerOnFieldControl : UserControl
    {
        private readonly MatchPlayer _player;
        private readonly string _imagePath;
        private readonly MatchData _matchData;

        public PlayerOnFieldControl(MatchPlayer player, MatchData matchData)
        {
            InitializeComponent();

            _player = player;
            _matchData = matchData;
            _imagePath = Utility.GetPlayerImagePath(player.Name);

            tbName.Text = player.Name;
            tbNumber.Text = $"{player.ShirtNumber}";
            imgPlayer.Source = new BitmapImage(new Uri(_imagePath, UriKind.RelativeOrAbsolute));
        }

        private void OnPlayerClick(object sender, MouseButtonEventArgs e)
        {
            ShowPlayerInfo(_player, _matchData);
        }

        private void ShowPlayerInfo(MatchPlayer player, MatchData matchData)
        {
            // Count goals and yellow cards for this player
            int goals = Utility.CalcGoalsForPlayerInMatch(player, matchData);
            int yellowCards = Utility.CalcYellowCardsForPlayerInMatch(player, matchData);

            Window window = new PlayerOverviewWindow(_player, _imagePath, goals, yellowCards);
            
            // Start slightly offscreen vertically
            window.Top += 100;
            window.Opacity = 0;
            window.Show();

            // Animate slide in and fade in simultaneously
            var slideIn = new DoubleAnimation(window.Top, window.Top - 100, TimeSpan.FromSeconds(0.3))
            {
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));

            window.BeginAnimation(Window.TopProperty, slideIn);
            window.BeginAnimation(Window.OpacityProperty, fadeIn);
        }
    }
}
