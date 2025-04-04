using DataLayer.Enums;
using DataLayer.Models;
using DataLayer.Models.Match;
using Newtonsoft.Json;

namespace DataLayer.Services
{
    public class ApiService : IDataService
    {
        // Instantiate the HttpClient and root url to the api
        private readonly HttpClient _httpClient;
        private const string apiRootUrl = "https://worldcup-vua.nullbit.hr/";

        //Dependency injection for HttpClient and set its BaseAddress
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(apiRootUrl);
        }

        public async Task<List<MatchData>> GetMatchDataAsync(Category category)
        {
            string url = $"{BuildBaseUrl(category)}/matches";

            return await FetchDataAsync<List<MatchData>>(url);
        }

        public async Task<List<MatchData>> GetMatchDataByCountryAsync(Category category, string fifaCode)
        {
            string url = $"{BuildBaseUrl(category)}/matches/country?fifa_code={fifaCode}";

            return await FetchDataAsync<List<MatchData>>(url);
        }

        public async Task<List<Team>> GetTeamsAsync(Category category)
        {
            string url = $"{BuildBaseUrl(category)}/teams";

            return await FetchDataAsync<List<Team>>(url);
        }

        public async Task<List<TeamResults>> GetTeamResultsAsync(Category category)
        {
            string url = $"{BuildBaseUrl(category)}/teams/results";

            return await FetchDataAsync<List<TeamResults>>(url);
        }

        public async Task<List<GroupResults>> GetGroupResultsAsync(Category category)
        {
            string url = $"{BuildBaseUrl(category)}/teams/group_results";

            return await FetchDataAsync<List<GroupResults>>(url);
        }

        //Construct the base url for requests based on category
        private string BuildBaseUrl(Category category)
            => $"{apiRootUrl}/{CategoryHelper.GetCategoryString(category)}";

        // Get API response individually from a url
        private async Task<HttpResponseMessage> GetApiResponseAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error fetching match data ({url}): {response.StatusCode}");

            return response;
        }

        // Return deserialized response
        private async Task<T> DeserializeResponseAsync<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        // Fetches and deserializes JSON data from a given API URL.
        private async Task<T> FetchDataAsync<T>(string url)
        {
            var response = await GetApiResponseAsync(url);
            return await DeserializeResponseAsync<T>(response);
        }
    }
}
