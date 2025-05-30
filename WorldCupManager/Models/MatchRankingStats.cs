using DataLayer.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupManager.Models
{
    public class MatchRankingStats : IComparable<MatchRankingStats>
    {
        public long FifaId { get; set; }
        public string Location { get; set; }
        public long Visitors { get; set; }
        public MatchTeam HomeTeam { get; set; }
        public MatchTeam AwayTeam { get; set; }

        // Order by number of visitors
        public int CompareTo(MatchRankingStats? other)
        {
            return -Visitors.CompareTo(other.Visitors);
        }

        public override string ToString()
            => $"{Location}, {Visitors}, {HomeTeam.Country}, {AwayTeam.Country}";

        public string FormatForPrinting()
            => $"Location: {Location}, Attendance: {Visitors}, Home Team: {HomeTeam.Country}, Away Team: {AwayTeam.Country}";

        public override bool Equals(object? obj)
        {
            return obj is MatchRankingStats stats &&
                   FifaId == stats.FifaId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FifaId);
        }
    }
}
