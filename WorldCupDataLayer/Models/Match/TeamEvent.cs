using Newtonsoft.Json;

namespace DataLayer.Models.Match
{
    public partial class TeamEvent : IComparable<TeamEvent>
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type_of_event")]
        public TypeOfEvent TypeOfEvent { get; set; }

        [JsonProperty("player")]
        public string Player { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        public int CompareTo(TeamEvent? other) 
            => Id.CompareTo(other.Id);

        public override bool Equals(object? obj)
        {
            return obj is TeamEvent @event &&
                   Id == @event.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
