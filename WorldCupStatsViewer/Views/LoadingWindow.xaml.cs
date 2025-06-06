using System.Windows;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace WorldCupStatsViewer.Views
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();

            Uri gifUri = new Uri("Pack://application:,,,/Resources/Images/Loading_icon.gif");
            BitmapImage img = new BitmapImage(gifUri);
            ImageBehavior.SetAnimatedSource(gifImg, img);
        }
    }
}
