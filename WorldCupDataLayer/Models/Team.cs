using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WorldCupDataLayer.Models
{
    public partial class Team : IComparable<Team>
    {
        public Team() { }

        public Team(long id, string country, string? alternateName, string fifaCode, long groupId, string groupLetter)
        {
            Id = id;
            Country = country;
            AlternateName = alternateName;
            FifaCode = fifaCode;
            GroupId = groupId;
            GroupLetter = groupLetter;
        }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("alternate_name")]
        public string? AlternateName { get; set; }

        [JsonProperty("fifa_code")]
        public string FifaCode { get; set; }

        [JsonProperty("group_id")]
        public long GroupId { get; set; }

        [JsonProperty("group_letter")]
        public string GroupLetter { get; set; }

        public int CompareTo(Team? other)
        {
            return Id.CompareTo(other.Id);
        }

        public override bool Equals(object? obj)
        {
            return obj is Team team &&
                   Id == team.Id &&
                   FifaCode == team.FifaCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FifaCode);
        }
    }
}
