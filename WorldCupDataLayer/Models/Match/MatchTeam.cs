using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Match
{
    public partial class MatchTeam : IComparable<MatchTeam>
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("code")]
        public string FifaCode { get; set; }

        [JsonProperty("goals")]
        public long Goals { get; set; }

        [JsonProperty("penalties")]
        public long Penalties { get; set; }

        public int CompareTo(MatchTeam? other)
        {
            return FifaCode.CompareTo(other.FifaCode);
        }

        public override bool Equals(object? obj)
        {
            return obj is MatchTeam team &&
                   FifaCode == team.FifaCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FifaCode);
        }

        public override string ToString()
            => $"{Country} ({FifaCode})";
    }
}
