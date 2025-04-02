using DataLayer.Enums;
using DataLayer.Models;
using DataLayer.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services.Local
{
    public interface ILocalDataService
    {
        Task<List<GroupResults>> LoadGroupResultsAsync(Category category);
        Task<List<MatchData>> LoadMatchesAsync(Category category);
        Task<List<TeamResults>> LoadTeamResultsAsync(Category category);
        Task<List<Team>> LoadTeamsAsync(Category category);
    }
}
