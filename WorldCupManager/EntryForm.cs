using DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            SaveUserSettings(category, language);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SaveUserSettings(Category category, string? language)
        {
            /*
                Since we check and make the file directories before opening this form
                I will not do the checking here, we are just putting our values into
                the file
            */

            File.WriteAllText(userSettingsPath, $"Category:{CategoryHelper.GetCategoryAsString(category)}\nLanguage:{language}");
        }
    }
}
