using DataLayer.Enums;
using DataLayer.Models;
using DataLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Utilities;

namespace WorldCupManager
{
    public partial class FavouriteTeamForm : Form
    {
        // Initialise the DataService (needed for loading team data)
        private IDataService _dataService;

        private IList<Team> _teams;
        private readonly Category _category;

        // On form load, "teams" gets filled, so warning ignored
        public FavouriteTeamForm(IDataService service)
        {
            InitializeComponent();
            _dataService = service;

            IDictionary<string, string> userSettings = Utility.LoadUserSettings();
            _category = CategoryHelper.GetCategory(userSettings["Category"]);
        }

        private async void FavouriteTeamForm_Load(object sender, EventArgs e)
        {
            using LoadingForm lf = new LoadingForm();
            lf.Show();
            lf.Refresh(); // Insure showing of loading animation

            try
            {
                // Fill teams from dataService
                _teams = await _dataService.GetTeamsAsync(_category);

                cbTeams.DisplayMember = "DisplayName";
                cbTeams.ValueMember = "FifaCode";
                cbTeams.DataSource = _teams.Select(team => new
                {
                    DisplayName = $"{team.Country} ({team.FifaCode})",
                    FifaCode = team.FifaCode
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load teams: {ex.Message}");
            }
            finally
            {
                lf.Close();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cbTeams.SelectedItem == null)
            {
                MessageBox.Show("Please select a team");
                return;
            }

            string? favouriteTeamCode = cbTeams.SelectedValue?.ToString();
            // Since SelectedValue can be null, we must check it
            if (string.IsNullOrWhiteSpace(favouriteTeamCode))
            {
                MessageBox.Show("Invalid selection");
                return;
            }

            try
            {
                Utility.SaveFavouriteTeam(favouriteTeamCode);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save team: {ex.Message}");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Return DialogResult.Cancel and close, program.cs handles the rest
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
