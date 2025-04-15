using DataLayer.Enums;
using Utilities;

namespace WorldCupManager
{
    public partial class EntryForm : Form
    {
        private readonly string userSettingsPath
            = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "userSettings.txt");
        public EntryForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cbCategory.SelectedItem == null || cbLanguage.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select from the available options",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return; //return if invalid form
            }

            Category category = CategoryHelper.GetCategory(cbCategory.SelectedItem.ToString());
            string language = cbLanguage.SelectedItem.ToString();

            Utility.SaveUserSettings(userSettingsPath, category, language);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
