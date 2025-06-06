using System.Windows;

namespace WorldCupStatsViewer.Views
{
    /// <summary>
    /// Interaction logic for ExitConfirmationWindow.xaml
    /// </summary>
    public partial class ExitConfirmationWindow : Window
    {
        public ExitConfirmationWindow()
        {
            InitializeComponent();
        }

        private void BtnConfirmExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void BtnDenyExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
