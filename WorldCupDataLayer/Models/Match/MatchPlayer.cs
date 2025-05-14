using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Match
{
    public partial class MatchPlayer : IComparable<MatchPlayer>
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("captain")]
        public bool Captain { get; set; }

        [JsonProperty("shirt_number")]
        public long ShirtNumber { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }

        public int CompareTo(MatchPlayer? other)
        {
            if (other is null)
                throw new ArgumentNullException("Error: null player supplied!");

            return ShirtNumber.CompareTo(other.ShirtNumber);
        }

        public override string ToString()
        {
            //      10 -    Luka Modric     Forward (Captain)
            //      5 -     other player    Midfield             *blank if not captain*
            return $"{ShirtNumber} - {Name}, {Position} {(Captain ? "(Captain)" : null)}";
        }

        public static MatchPlayer Parse(string line)
        {
            try
            {
                // Split on the first " - "
                // { (shirtNumber), (rest of player info) }
                string[] numberSplit = line.Split(" - ", 2);
                if (numberSplit.Length < 2)
                    throw new FormatException("Invalid format: missing dash");

                int shirtNumber = int.Parse(numberSplit[0].Trim());

                // Split second part into name and rest, including position and potentially (Captain)
                string[] nameSplit = numberSplit[1].Split(',', 2);
                if (nameSplit.Length < 2)
                    throw new FormatException("Invalid format: missing comma");

                // Separate into variables
                string name = nameSplit[0].Trim();
                string positionAndCaptain = nameSplit[1].Trim();

                bool isCaptain = positionAndCaptain.EndsWith("(Captain)");

                // Ensure position variable is left with correct info, 
                string positionString = isCaptain
                    // Captain info saved, so remove the tag and store in position variable
                    ? positionAndCaptain.Replace("(Captain)", "").Trim()
                    // Nothing to trim from previously saved variable
                    : positionAndCaptain;

                // Parse position into valid Position enum
                if (!Enum.TryParse(positionString, out Position position))
                    throw new FormatException($"Invalid position: {positionString}");

                return new MatchPlayer
                {
                    Name = name,
                    ShirtNumber = shirtNumber,
                    Position = position,
                    Captain = isCaptain
                };
            }
            catch (Exception ex)
            {
                throw new FormatException($"Failed to parse MatchPlayer: {ex.Message}");
            }
        }
    }
}
