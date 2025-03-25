using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupDataLayer.Models
{
    public partial class Match
    {
        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("fifa_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FifaId { get; set; }

        [JsonProperty("weather")]
        public Weather Weather { get; set; }

        [JsonProperty("attendance")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Attendance { get; set; }

        [JsonProperty("officials")]
        public string[] Officials { get; set; }

        [JsonProperty("stage_name")]
        public string StageName { get; set; }

        [JsonProperty("home_team_country")]
        public string HomeTeamCountry { get; set; }

        [JsonProperty("away_team_country")]
        public string AwayTeamCountry { get; set; }

        [JsonProperty("datetime")]
        public DateTimeOffset Datetime { get; set; }

        [JsonProperty("winner")]
        public string Winner { get; set; }

        [JsonProperty("winner_code")]
        public string WinnerCode { get; set; }

        [JsonProperty("home_team")]
        public MatchTeam HomeTeam { get; set; }

        [JsonProperty("away_team")]
        public MatchTeam AwayTeam { get; set; }

        [JsonProperty("home_team_events")]
        public TeamEvent[] HomeTeamEvents { get; set; }

        [JsonProperty("away_team_events")]
        public TeamEvent[] AwayTeamEvents { get; set; }

        [JsonProperty("home_team_statistics")]
        public TeamStatistics HomeTeamStatistics { get; set; }

        [JsonProperty("away_team_statistics")]
        public TeamStatistics AwayTeamStatistics { get; set; }

        [JsonProperty("last_event_update_at")]
        public DateTimeOffset LastEventUpdateAt { get; set; }

        [JsonProperty("last_score_update_at")]
        public object LastScoreUpdateAt { get; set; }
    }

    public class MatchTeam
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("code")]
        public string FifaCode { get; set; }

        [JsonProperty("goals")]
        public long Goals { get; set; }

        [JsonProperty("penalties")]
        public long Penalties { get; set; }
    }

    public partial class TeamEvent
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type_of_event")]
        public TypeOfEvent TypeOfEvent { get; set; }

        [JsonProperty("player")]
        public string Player { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }
    }

    public partial class TeamStatistics
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("attempts_on_goal")]
        public long AttemptsOnGoal { get; set; }

        [JsonProperty("on_target")]
        public long OnTarget { get; set; }

        [JsonProperty("off_target")]
        public long OffTarget { get; set; }

        [JsonProperty("blocked")]
        public long Blocked { get; set; }

        [JsonProperty("woodwork")]
        public long Woodwork { get; set; }

        [JsonProperty("corners")]
        public long Corners { get; set; }

        [JsonProperty("offsides")]
        public long Offsides { get; set; }

        [JsonProperty("ball_possession")]
        public long BallPossession { get; set; }

        [JsonProperty("pass_accuracy")]
        public long PassAccuracy { get; set; }

        [JsonProperty("num_passes")]
        public long NumPasses { get; set; }

        [JsonProperty("passes_completed")]
        public long PassesCompleted { get; set; }

        [JsonProperty("distance_covered")]
        public long DistanceCovered { get; set; }

        [JsonProperty("balls_recovered")]
        public long BallsRecovered { get; set; }

        [JsonProperty("tackles")]
        public long Tackles { get; set; }

        [JsonProperty("clearances")]
        public long Clearances { get; set; }

        [JsonProperty("yellow_cards")]
        public long YellowCards { get; set; }

        [JsonProperty("red_cards")]
        public long RedCards { get; set; }

        [JsonProperty("fouls_committed")]
        public long FoulsCommitted { get; set; }

        [JsonProperty("tactics")]
        public string Tactics { get; set; }

        [JsonProperty("starting_eleven")]
        public MatchPlayer[] StartingEleven { get; set; }

        [JsonProperty("substitutes")]
        public MatchPlayer[] Substitutes { get; set; }
    }

    public partial class MatchPlayer
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("captain")]
        public bool Captain { get; set; }

        [JsonProperty("shirt_number")]
        public long ShirtNumber { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }
    }

    public partial class Weather
    {
        [JsonProperty("humidity")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Humidity { get; set; }

        [JsonProperty("temp_celsius")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TempCelsius { get; set; }

        [JsonProperty("temp_farenheit")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TempFarenheit { get; set; }

        [JsonProperty("wind_speed")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long WindSpeed { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public enum TypeOfEvent { Goal, SubstitutionIn, SubstitutionOut, YellowCard };
    public enum Position { Defender, Forward, Goalie, Midfield };

    // This static class provides a centralized location for JSON serialization settings.
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            // Ignores metadata properties that might be included in JSON but are not needed in our C# classes.
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,

            // Prevents automatic parsing of dates into DateTime objects (useful if dates are in string format).
            DateParseHandling = DateParseHandling.None,

            // Specifies custom converters for handling special data types.
            Converters =
        {
            TypeOfEventConverter.Singleton, // Converts TypeOfEvent enum to/from JSON string.
            PositionConverter.Singleton,    // Converts Position enum to/from JSON string.
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal } // Ensures proper date handling.
        },
        };
    }

    // A custom JSON converter to handle cases where numeric values (long) are represented as strings in JSON.
    internal class ParseStringConverter : JsonConverter
    {
        // Determines whether this converter can be used for a given type.
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        // Custom deserialization: Converts JSON string to a long (integer).
        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null; // Handles null values.

            var value = serializer.Deserialize<string>(reader); // Read the JSON value as a string.
            if (long.TryParse(value, out long l)) // Try converting the string to a long.
            {
                return l; // Return the parsed long value.
            }

            throw new Exception("Cannot unmarshal type long"); // Error handling if conversion fails.
        }

        // Custom serialization: Converts long (integer) to a string before writing to JSON.
        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString()); // Converts long to string for JSON output.
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter(); // Singleton instance.
    }

    // Custom converter to handle conversion between JSON string and the TypeOfEvent enum.
    internal class TypeOfEventConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeOfEvent) || t == typeof(TypeOfEvent?);

        // Converts JSON string to TypeOfEvent enum during deserialization.
        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            var value = serializer.Deserialize<string>(reader); // Read the value as a string.
            switch (value)
            {
                case "goal":
                    return TypeOfEvent.Goal;
                case "substitution-in":
                    return TypeOfEvent.SubstitutionIn;
                case "substitution-out":
                    return TypeOfEvent.SubstitutionOut;
                case "yellow-card":
                    return TypeOfEvent.YellowCard;
            }

            throw new Exception("Cannot unmarshal type TypeOfEvent");
        }

        // Converts TypeOfEvent enum back to a JSON string during serialization.
        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            var value = (TypeOfEvent)untypedValue;
            switch (value)
            {
                case TypeOfEvent.Goal:
                    serializer.Serialize(writer, "goal");
                    return;
                case TypeOfEvent.SubstitutionIn:
                    serializer.Serialize(writer, "substitution-in");
                    return;
                case TypeOfEvent.SubstitutionOut:
                    serializer.Serialize(writer, "substitution-out");
                    return;
                case TypeOfEvent.YellowCard:
                    serializer.Serialize(writer, "yellow-card");
                    return;
            }

            throw new Exception("Cannot marshal type TypeOfEvent");
        }

        public static readonly TypeOfEventConverter Singleton = new TypeOfEventConverter();
    }

    // Custom converter to handle conversion between JSON string and the Position enum.
    internal class PositionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Position) || t == typeof(Position?);

        // Converts JSON string to Position enum during deserialization.
        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Defender":
                    return Position.Defender;
                case "Forward":
                    return Position.Forward;
                case "Goalie":
                    return Position.Goalie;
                case "Midfield":
                    return Position.Midfield;
            }

            throw new Exception("Cannot unmarshal type Position");
        }

        // Converts Position enum back to a JSON string during serialization.
        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            var value = (Position)untypedValue;
            switch (value)
            {
                case Position.Defender:
                    serializer.Serialize(writer, "Defender");
                    return;
                case Position.Forward:
                    serializer.Serialize(writer, "Forward");
                    return;
                case Position.Goalie:
                    serializer.Serialize(writer, "Goalie");
                    return;
                case Position.Midfield:
                    serializer.Serialize(writer, "Midfield");
                    return;
            }

            throw new Exception("Cannot marshal type Position");
        }

        public static readonly PositionConverter Singleton = new PositionConverter();
    }

}
