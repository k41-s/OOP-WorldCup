using DataLayer.Models.Match;
using DataLayer.Services;
using Utilities;

namespace WorldCupManager.Models
{
    public class PlayerRankingStats
    {
        public MatchPlayer Player { get; set; }
        public int Goals { get; set; }
        public int YellowCards { get; set; }
        public int Appearances { get; set; }
        public string ImagePath { get; set; }

        public override string ToString()
            => $"{Player.Name}, {Goals}, {YellowCards}, {Appearances}";

        public string FormatForPrinting()
            => $"Player: {Player.Name}, Goals Scored: {Goals}, Yellow Cards: {YellowCards}, Appearances: {Appearances}";

        public override bool Equals(object? obj)
        {
            return obj is PlayerRankingStats stats &&
                   EqualityComparer<MatchPlayer>.Default.Equals(Player, stats.Player);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Player);
        }
    }
}
