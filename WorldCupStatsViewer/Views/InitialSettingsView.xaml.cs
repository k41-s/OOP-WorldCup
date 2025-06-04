using DataLayer.Enums;
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
using System.Windows.Shapes;
using Utilities;

namespace WorldCupStatsViewer.Views
{
    /// <summary>
    /// Interaction logic for InitialSettingsView.xaml
    /// </summary>
    public partial class InitialSettingsView : Window
    {
        private Category category;
        private string? language;
        private string? displayMode;

        public InitialSettingsView()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string? selectedGender = ((ComboBoxItem)cbGender.SelectedItem).Content.ToString();
            if (selectedGender != null)
                category = CategoryHelper.GetCategory(selectedGender);

            language = ((ComboBoxItem)cbLanguage.SelectedItem).Content.ToString();
            displayMode = ((ComboBoxItem)cbDisplay.SelectedItem).Content.ToString();

            if (language != null && displayMode != null)
            {
                try
                {
                    Utility.SaveUserSettings(category, language, displayMode);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            this.DialogResult = true;
            this.Close();
        }
    }
}
