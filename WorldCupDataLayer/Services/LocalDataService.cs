using DataLayer.Enums;
using DataLayer.Models;
using DataLayer.Models.Match;
using Newtonsoft.Json;

namespace DataLayer.Services
{
    public class LocalDataService : IDataService
    {
        // Dynamically determine at runtime the path to the WorldCupData folder
        private static readonly string DataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WorldCupData");

        public async Task<List<GroupResults>> GetGroupResultsAsync(Category category)
        {
            string filePath = GetFilePath(category, "group_results.json");
            return await ReadFromFileAsync<List<GroupResults>>(filePath);
        }


        public async Task<List<MatchData>> GetMatchDataAsync(Category category)
        {
            string filePath = GetFilePath(category, "matches.json");
            return await ReadFromFileAsync<List<MatchData>>(filePath);
        }

        public async Task<List<MatchData>> GetMatchDataByCountryAsync(Category category, string fifaCode)
        {
            var allMatches = await GetMatchDataAsync(category);

            return allMatches
                .Where(m => m.HomeTeam.FifaCode.Equals(fifaCode, StringComparison.OrdinalIgnoreCase) ||
                            m.AwayTeam.FifaCode.Equals(fifaCode, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public async Task<List<TeamResults>> GetTeamResultsAsync(Category category)
        {
            string filePath = GetFilePath(category, "results.json");
            return await ReadFromFileAsync<List<TeamResults>>(filePath);
        }

        public async Task<List<Team>> GetTeamsAsync(Category category)
        {
            string filePath = GetFilePath(category, "teams.json");
            return await ReadFromFileAsync<List<Team>>(filePath);
        }

        // Helper method to generate the correct file path based on category (Men/Women)
        private string GetFilePath(Category category, string fileName) 
            => Path.Combine(DataFolder, CategoryHelper.GetCategoryString(category), fileName);

        // Helper method to read from a JSON file and deserialize into the specified type
        private async Task<T> ReadFromFileAsync<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"The file '{filePath}' does not exist");

            string json = await File.ReadAllTextAsync(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
