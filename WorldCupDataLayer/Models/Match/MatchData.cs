using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace DataLayer.Models.Match
{
    public partial class MatchData : IComparable<MatchData>
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
        public List<string> Officials { get; set; }

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
        public List<TeamEvent> HomeTeamEvents { get; set; }

        [JsonProperty("away_team_events")]
        public List<TeamEvent> AwayTeamEvents { get; set; }

        [JsonProperty("home_team_statistics")]
        public TeamStatistics HomeTeamStatistics { get; set; }

        [JsonProperty("away_team_statistics")]
        public TeamStatistics AwayTeamStatistics { get; set; }

        [JsonProperty("last_event_update_at")]
        public DateTimeOffset LastEventUpdateAt { get; set; }

        [JsonProperty("last_score_update_at")]
        public DateTimeOffset? LastScoreUpdateAt { get; set; }

        public int CompareTo(MatchData? other)
        {
            return FifaId.CompareTo(other.FifaId);
        }

        public override bool Equals(object? obj)
        {
            return obj is MatchData match &&
                   FifaId == match.FifaId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FifaId);
        }
    }

    [JsonConverter(typeof(StringEnumConverter), true)] // true allows case in-sensitive
    public enum TypeOfEvent 
    {
        [EnumMember(Value = "goal")]
        Goal,

        [EnumMember(Value = "goal-own")]
        GoalOwn,

        [EnumMember(Value = "goal-penalty")]
        GoalPenalty,

        [EnumMember(Value = "substitution-in")]
        SubstitutionIn,

        [EnumMember(Value = "substitution-out")]
        SubstitutionOut,

        [EnumMember(Value = "yellow-card")]
        YellowCard,

        [EnumMember(Value = "yellow-card-second")]
        YellowCardSecond,

        [EnumMember(Value = "red-card")]
        RedCard 
    };

    [JsonConverter(typeof(StringEnumConverter), true)] // true allows case in-sensitive
    public enum Position
    {
        [EnumMember(Value = "Defender")]
        Defender,

        [EnumMember(Value = "Forward")]
        Forward,

        [EnumMember(Value = "Goalie")]
        Goalie,

        [EnumMember(Value = "Midfield")]
        Midfield 
    };
}
