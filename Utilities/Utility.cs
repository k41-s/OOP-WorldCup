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
        private static readonly string configFolderPath
            = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");

        // Path to userSettings file
        private static readonly string userSettingsPath
            = Path.Combine(configFolderPath, "userSettings.txt");

        // Load user settings from file
        public static IDictionary<string, string> LoadUserSettings()
        {
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
        public static void SaveUserSettings(string filePath, Category category, string? language)
        {
            // No error checking needed, check File.WriteAllText documentation
            File.WriteAllText(filePath, $"Category:{CategoryHelper.GetCategoryAsString(category)}\nLanguage:{language}");
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
    }
}
