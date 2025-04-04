using DataLayer.Enums;
using DataLayer.Models.Match;
using DataLayer.Models;

namespace DataLayer.Services
{
    public interface IDataService
    {
        Task<List<MatchData>> GetMatchDataAsync(Category category);
        Task<List<MatchData>> GetMatchDataByCountryAsync(Category category, string fifaCode);
        Task<List<Team>> GetTeamsAsync(Category category);
        Task<List<TeamResults>> GetTeamResultsAsync(Category category);
        Task<List<GroupResults>> GetGroupResultsAsync(Category category);
    }
}
