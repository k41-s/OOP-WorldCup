using DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Utility
    {
        // Delimiter in config file
        private const char FILE_DEL = ':';

        // Path to Config folder
        public static readonly string configFolderPath
            = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");

        // Path to userSettings file
        public static readonly string userSettingsPath
            = Path.Combine(configFolderPath, "userSettings.txt");

        // Path to favourite team file
        public static readonly string favouriteTeamPath
            = Path.Combine(configFolderPath, "favouriteTeam.txt");

        // Load user settings from file
        public static IDictionary<string, string> LoadUserSettings()
        {
            // OrdinalIgnoreCase will allow the keys to be accessed with any case
            IDictionary<string, string> settings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (!File.Exists(userSettingsPath))
                return settings;

            string[] lines = File.ReadAllLines(userSettingsPath);

            foreach (string line in lines)
            {
                if (String.IsNullOrWhiteSpace(line))
                    continue;

                string[] data = line.Split(FILE_DEL);

                if(data.Length == 2)
                {
                    if (!settings.ContainsKey(data[0]))
                        settings.Add(data[0], data[1]);
                }
            }

            return settings;
        }

        // Save selected category and language to file
        public static void SaveUserSettings(Category category, string? language)
        {
            // No error checking needed, check File.WriteAllText documentation
            File.WriteAllText(userSettingsPath, 
                $"Category:{CategoryHelper.GetCategoryAsString(category)}\nLanguage:{language}");
        }

        // Read "UseApi" status from config file
        public static bool UseApiService()
        {
            string filePath = Path.Combine(configFolderPath, "config.txt");

            bool useApi = false; // Default to local service

            // if file exists execute operations
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                // Access each element from the file
                lines.ToList().ForEach(line =>
                {
                    string[] details = line.Split(FILE_DEL); // key:value pair from config file

                    if (details[0].ToLower() == "useapi")
                        // Successful parse && value from config
                        useApi = bool.TryParse(details[1], out bool result) && result;
                });
            }
            return useApi;
        }

        // Save favourite team's fifaCode to the file
        public static void SaveFavouriteTeam(string fifaCode)
        {
            File.WriteAllText(favouriteTeamPath, $"FavTeam:{fifaCode}");
        }

        // Load the favourite team's fifaCode from a file
        public static string? LoadFavouriteTeamCode()
        {
            if (!File.Exists(favouriteTeamPath))
                throw new FileNotFoundException($"File: {favouriteTeamPath} does not exist");

            string[] lines = File.ReadAllLines(favouriteTeamPath);

            foreach (var line in lines)
            {
                string[] details = line.Split(FILE_DEL);

                if (details.Length == 2
                    && details[0].ToLower() == "favteam")
                    return details[1];
            }

            return null; // If no saved data on team just return null
        }
    }
}
