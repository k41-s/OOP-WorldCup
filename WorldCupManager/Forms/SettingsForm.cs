using DataLayer.Enums;
using Utilities;

namespace WorldCupManager
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitForm();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ConfirmSettings();
        }

        private void SettingsForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ConfirmSettings();
            else if (e.KeyCode == Keys.Escape)
                ExitForm();
        }

        private void ConfirmSettings()
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

            Utility.SaveUserSettings(category, language);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ExitForm()
        {
            using ExitConfirmationForm form = new();
            DialogResult dialogResult = form.ShowDialog();

            // Ok for confirmation of exiting
            if (dialogResult == DialogResult.OK)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else if (dialogResult == DialogResult.Cancel) // Decided not to leave application
                return;
        }
    }
}
