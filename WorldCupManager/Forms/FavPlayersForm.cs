using DataLayer.Enums;
using DataLayer.Models;
using DataLayer.Models.Match;
using DataLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using Utilities;

namespace WorldCupManager
{
    public partial class FavPlayersForm : Form
    {
        // Init data service to be used
        private IDataService _service;

        private IList<MatchPlayer> _playersLoaded; // Loaded on form load, "master" list

        private IList<MatchPlayer> _allPlayers; // Backing list for flpAllPlayers
        private IList<MatchPlayer> _favPlayers; // Backing list for flpFavPlayers

        // Store selected category and fav team
        private readonly Category _category;

        public FavPlayersForm(IDataService service)
        {
            InitializeComponent();

            _service = service;

            IDictionary<string, string> userSettings = Utility.LoadUserSettings();
            _category = CategoryHelper.GetCategory(userSettings["Category"]);
        }

        private async void FavPlayersForm_Shown(object sender, EventArgs e)
        {
            using LoadingForm lf = new LoadingForm();
            lf.Show();
            lf.Refresh(); // Make sure animation displayed

            try
            {
                _playersLoaded = await Task.Run(async () =>
                {
                    string? teamCode = Utility.LoadFavouriteTeamCode();

                    if (string.IsNullOrEmpty(teamCode))
                        throw new InvalidOperationException("No favourite team selected.");

                    return await GetPlayersSorted(teamCode);
                });

                _allPlayers = _playersLoaded;
                _favPlayers = new List<MatchPlayer>();

                foreach (var player in _allPlayers)
                {
                    Control control = new PlayerControl(player);
                    control.ContextMenuStrip = contextMenuStrip;

                    flpAllPlayers.Controls.Add(control);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading players: {ex.Message}");

                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
            finally
            {
                lf.Close();
            }
        }

        private async Task<IList<MatchPlayer>> GetPlayersSorted(string? teamCode)
        {
            if (string.IsNullOrEmpty(teamCode))
                return new List<MatchPlayer>();

            IList<MatchData> matches = await _service.GetMatchDataAsync(_category);

            return await Task.Run(() =>
            {
                MatchData? firstMatch =
                    matches.FirstOrDefault(m => m.HomeTeam.FifaCode == teamCode 
                    || m.AwayTeam.FifaCode == teamCode);

                if (firstMatch == null)
                    return new List<MatchPlayer>();

                bool isHomeTeam = firstMatch.HomeTeam.FifaCode == teamCode;

                TeamStatistics stats = isHomeTeam
                    ? firstMatch.HomeTeamStatistics
                    : firstMatch.AwayTeamStatistics;

                return stats.StartingEleven
                    .Union(stats.Substitutes)
                    .Order()
                    .ToList();
            });
        }


        // At this point loading the whole team data here is unnecessary, but func will stay in case it is needed
        public async Task<Team>? LoadFavouriteTeam()
        {
            string? teamCode = Utility.LoadFavouriteTeamCode();

            // Separate loading teams and return value due to async nature of GetTeamsAsync
            IList<Team> teams = await _service.GetTeamsAsync(_category);

            return teams.FirstOrDefault(t => t.FifaCode == teamCode); // return team with matching fifaCode
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (contextMenuStrip.SourceControl is not PlayerControl pc)
            {
                e.Cancel = true;
                return;
            }

            bool isFavList = pc.Parent == flpFavPlayers;

            // Only show necessary menu items
            markFavouriteToolStripMenuItem.Visible = !isFavList;
            unmarkFavouriteToolStripMenuItem.Visible = isFavList;
        }

        private void MovePlayer(MatchPlayer player, IList<MatchPlayer> fromList,
            IList<MatchPlayer> toList, FlowLayoutPanel fromPanel, FlowLayoutPanel toPanel)
        {
            if (player == null || !fromList.Contains(player) || toList.Contains(player))
                return;

            // Limit favourite players to three
            if (toPanel == flpFavPlayers && toList.Count >= 3)
            {
                MessageBox.Show("You cannot select more than 3 favourite players.");
                return;
            }

            fromList.Remove(player);
            toList.Add(player);

            // Reset fromPanel contents
            fromPanel.Controls.Clear();
            fromList.Order().ToList().ForEach(p =>
            {
                Control pc = new PlayerControl(p) { ContextMenuStrip = contextMenuStrip };
                fromPanel.Controls.Add(pc);
            });

            // Reset toPanel contents
            toPanel.Controls.Clear();
            toList.Order().ToList().ForEach(p =>
            {
                Control pc = new PlayerControl(p) { ContextMenuStrip = contextMenuStrip };
                toPanel.Controls.Add(pc);
            });
        }

        private void markFavouriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contextMenuStrip.SourceControl is not PlayerControl pc)
                return;

            MatchPlayer player = pc._player;

            if (_allPlayers.Contains(player))
                MovePlayer(player, _allPlayers, _favPlayers, flpAllPlayers, flpFavPlayers);
        }


        private void unmarkFavouriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contextMenuStrip.SourceControl is not PlayerControl pc)
                return;

            MatchPlayer player = pc._player;

            if (_favPlayers.Contains(player))
                MovePlayer(player, _favPlayers, _allPlayers, flpFavPlayers, flpAllPlayers);
        }

        private void FlowLayoutPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PlayerControl)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void FlowLayoutPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(PlayerControl)) is not PlayerControl draggedPlayer)
                return;

            FlowLayoutPanel fromPanel = draggedPlayer.Parent as FlowLayoutPanel;
            FlowLayoutPanel toPanel = sender as FlowLayoutPanel;

            if (fromPanel == null || toPanel == null || fromPanel == toPanel)
                return;

            bool fromAll = fromPanel == flpAllPlayers;
            IList<MatchPlayer> fromList = fromAll ? _allPlayers : _favPlayers;
            IList<MatchPlayer> toList = fromAll ? _favPlayers : _allPlayers;

            MovePlayer(draggedPlayer._player, fromList, toList, fromPanel, toPanel);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Ensure three players are saved
            if (_favPlayers.Count != 3)
            {
                MessageBox.Show("You must select three favourite players!");
                return;
            }

            try
            {
                Utility.SaveFavouritePlayers(_favPlayers);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving players: {ex.Message}");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
