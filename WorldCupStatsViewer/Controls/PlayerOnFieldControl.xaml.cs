using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utilities;

namespace WorldCupStatsViewer.Controls
{
    /// <summary>
    /// Interaction logic for PlayerOnFieldControl.xaml
    /// </summary>
    public partial class PlayerOnFieldControl : UserControl
    {
        public PlayerOnFieldControl(string name, long number, string? imagePath = null)
        {
            InitializeComponent();
            txtName.Text = name;
            txtNumber.Text = $"{number}";

            string path = string.IsNullOrEmpty(imagePath)
            ? Utility.defaultNoPlayerImgPath
            : imagePath;

            imgPlayer.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
        }
    }
}
