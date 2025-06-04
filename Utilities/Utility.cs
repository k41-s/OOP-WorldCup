using DataLayer.Enums;
using DataLayer.Models.Match;

namespace Utilities
{
    public static class Utility
    {
        // Delimiter in config file
        private const char FILE_DEL = ':';

        // Path to Shared folder in Data Layer
        public static readonly string sharedFolderPath
            = GetSharedFolderPath();

        // Path to Shared images folder
        public static readonly string imagesFolderPath
            = Path.Combine(sharedFolderPath, "Images");

        // Path to default player image
        public static readonly string defaultNoPlayerImgPath
            = Path.Combine(imagesFolderPath, "No-Player-Img.png");

        // Path to Config folder
        public static readonly string configFolderPath
            = Path.Combine(sharedFolderPath, "Config");

        // Path to userSettings file
        public static readonly string userSettingsPath
            = Path.Combine(configFolderPath, "userSettings.txt");

        // Path to favourite team file
        public static readonly string favouriteTeamPath
            = Path.Combine(configFolderPath, "favouriteTeam.txt");

        //Path to favourite players file
        public static readonly string favouritePlayersPath
            = Path.Combine(configFolderPath, "favouritePlayers.txt");

        // Use data layer as a shared directory for relative paths
        public static string GetSharedFolderPath()
        {
            // Traverse up from current directory to find the solution root
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine("Starting directory: " + currentDir);
            DirectoryInfo? dir = new DirectoryInfo(currentDir);

            while (dir != null && !Directory.Exists(Path.Combine(dir.FullName, "WorldCupDataLayer", "Shared")))
            {
                dir = dir.Parent;
            }

            if (dir == null)
                throw new DirectoryNotFoundException("Could not locate DataLayer/Shared folder.");

            return Path.Combine(dir.FullName, "WorldCupDataLayer", "Shared");
        }

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
                if (string.IsNullOrWhiteSpace(line))
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
            // Make sure the Config folder exists
            Directory.CreateDirectory(configFolderPath);

            // No error checking needed, check File.WriteAllText documentation
            File.WriteAllText(userSettingsPath, 
                $"Category:{CategoryHelper.GetCategoryAsString(category)}\nLanguage:{language}");
        }

        // New for WPF, added displayMode setting
        public static void SaveUserSettings(Category category, string? language, string? displayMode = null)
        {
            // Make sure the Config folder exists
            Directory.CreateDirectory(configFolderPath);

            var lines = new List<string>
            {
                $"Category:{CategoryHelper.GetCategoryAsString(category)}",
                $"Language:{language}"
            };

            if (!string.IsNullOrWhiteSpace(displayMode))
                lines.Add($"DisplayMode:{displayMode}");

            File.WriteAllLines(userSettingsPath, lines);
        }

        public static string? LoadDisplayMode()
        {
            var settings = LoadUserSettings();
            return settings.TryGetValue("DisplayMode", out var mode) ? mode : null;
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

        public static MatchTeam? GetTeamByFifaCode(string? fifaCode, IList<MatchData> matches)
        {
            if (fifaCode is not null)
            {
                foreach (var match in matches)
                {
                    if (match.HomeTeam.FifaCode.Equals(fifaCode, StringComparison.OrdinalIgnoreCase))
                        return match.HomeTeam;
                    else if (match.AwayTeam.FifaCode.Equals(fifaCode, StringComparison.OrdinalIgnoreCase))
                        return match.AwayTeam;
                }

                return null; // Not found
            }

            return null; // fifaCode null
        }

        public static string? GetFifaCodeByTeamName(string? teamName, IList<MatchData> matches)
        {
            if (string.IsNullOrWhiteSpace(teamName) || matches == null || matches.Count == 0)
                return null;

            foreach (var match in matches)
            {
                if (match.HomeTeamCountry.Equals(teamName, StringComparison.OrdinalIgnoreCase))
                    return match.HomeTeam?.FifaCode;

                if (match.AwayTeamCountry.Equals(teamName, StringComparison.OrdinalIgnoreCase))
                    return match.AwayTeam?.FifaCode;
            }

            return null; // Not found
        }

        public static IList<MatchTeam> GetOpponents(string? fifaCode, IList<MatchData> allMatches)
            => allMatches
                        .Where(m => m.HomeTeam.FifaCode == fifaCode || m.AwayTeam.FifaCode == fifaCode)
                        .Select(m => m.HomeTeam.FifaCode == fifaCode ? m.AwayTeam : m.HomeTeam)
                        .Distinct()
                        .ToList();

        public static string GetPlayerImagePath(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                throw new ArgumentException("Player name is required");

            string fileName = $"{FormatFileName(playerName)}.png"; // Standardise png file type
            string imgPath = Path.Combine(imagesFolderPath, fileName);

            return File.Exists(imgPath)
                ? imgPath
                : defaultNoPlayerImgPath;
        }

        public static void SetPlayerImage(string sourceImgPath, string playerName)
        {
            // Format name and surname here to be safe
            string fileName = $"{FormatFileName($"{playerName}")}.png";
            string destPath = Path.Combine(imagesFolderPath, fileName);

            File.Copy(sourceImgPath, destPath, overwrite: true);
        }

        // Make sure file name is standardised and legitimate
        private static string FormatFileName(string input)
        {
            // Replace invalid chars with '_';
            foreach (char c in Path.GetInvalidPathChars())
                input = input.Replace(c, '_');

            return input.Trim().ToLower().Replace(' ', '_');
        }

        public static void SaveFavouritePlayers(IList<MatchPlayer> favPlayers)
        {
            File.WriteAllLines(favouritePlayersPath, favPlayers.Select(p => p.ToString()));
        }

        public static int CalcGoalsForTeam(MatchData match, string fifaCode)
        {
            int goals = 0;

            // Check if the team is home or away
            bool isHomeTeam = match.HomeTeam?.FifaCode == fifaCode;
            bool isAwayTeam = match.AwayTeam?.FifaCode == fifaCode;

            if (!isHomeTeam && !isAwayTeam)
                return 0; // Team not in this match

            // Assign appropriate events to lists
            IList<TeamEvent> ownEvents = isHomeTeam ? match.HomeTeamEvents : match.AwayTeamEvents;
            IList<TeamEvent> opponentEvents = isAwayTeam ? match.AwayTeamEvents : match.HomeTeamEvents;

            // Our team's goals
            goals += ownEvents.Count(e =>
                e.TypeOfEvent == TypeOfEvent.Goal ||
                e.TypeOfEvent == TypeOfEvent.GoalPenalty
            );

            // Opponent's own goals which count for this team
            goals += opponentEvents.Count(e => e.TypeOfEvent == TypeOfEvent.GoalOwn);

            return goals;
        }

        public static int CalcGoalsForPlayer(MatchPlayer player, IList<MatchData> matches, string teamName)
        {
            int numGoals = 0;

            foreach(var match in matches)
            {
                List<TeamEvent>? relevantEvents = null;

                if (match.HomeTeamCountry == teamName)
                    relevantEvents = match.HomeTeamEvents;
                else if (match.AwayTeamCountry == teamName)
                    relevantEvents = match.AwayTeamEvents;
                else
                    continue;

                    numGoals += relevantEvents.Count(e =>
                        e.Player.Equals(player.Name, StringComparison.OrdinalIgnoreCase) &&
                        (e.TypeOfEvent == TypeOfEvent.Goal 
                        || e.TypeOfEvent == TypeOfEvent.GoalPenalty));
            };

            return numGoals;
        }

        public static int CalcYellowCardsForPlayer(MatchPlayer player, IList<MatchData> matches, string teamName)
        {
            int yellowCards = 0;

            foreach (var match in matches)
            {
                List<TeamEvent>? relevantEvents = null;

                if (match.HomeTeamCountry == teamName)
                    relevantEvents = match.HomeTeamEvents;
                else if (match.AwayTeamCountry == teamName)
                    relevantEvents = match.AwayTeamEvents;
                else
                    continue;

                yellowCards += relevantEvents.Count(e =>
                    e.Player.Equals(player.Name, StringComparison.OrdinalIgnoreCase) &&
                    (e.TypeOfEvent == TypeOfEvent.YellowCard 
                    || e.TypeOfEvent == TypeOfEvent.YellowCardSecond));
            };

            return yellowCards;
        }

        public static int CalcAppearancesForPlayer(MatchPlayer player, IList<MatchData> matches, string teamName)
        {
            int appearances = 0;

            foreach (var match in matches)
            {
                TeamStatistics? relevantStats = match.HomeTeamCountry == teamName
                    ? match.HomeTeamStatistics
                    : match.AwayTeamStatistics;

                if (relevantStats == null)
                    continue;

                bool appeared = relevantStats.StartingEleven.Any(p =>
                    p.Name.Equals(player.Name, StringComparison.OrdinalIgnoreCase))
                    || relevantStats.Substitutes.Any(p =>
                    p.Name.Equals(player.Name, StringComparison.OrdinalIgnoreCase));

                if (appeared) appearances++;
            }

            return appearances;
        }
    }
}
