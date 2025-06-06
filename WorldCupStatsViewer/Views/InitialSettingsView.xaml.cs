using DataLayer.Enums;
using System.Windows;
using System.Windows.Controls;
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
            SaveSettings();

            this.DialogResult = true;
            this.Close();
        }

        private void SaveSettings()
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
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
