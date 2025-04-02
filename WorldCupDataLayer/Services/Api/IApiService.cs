using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Enums;
using DataLayer.Models;
using DataLayer.Models.Match;

namespace DataLayer.Services.Api
{
    public interface IApiService
    {
        Task<List<MatchData>> GetMatchDataAsync(Category category);
        Task<List<MatchData>> GetMatchDataByCountryAsync(Category category, string fifaCode);
        Task<List<Team>> GetTeamsAsync(Category category);
        Task<List<TeamResults>> GetTeamResultsAsync(Category category);
        Task<List<GroupResults>> GetGroupResultsAsync(Category category);    }
}
