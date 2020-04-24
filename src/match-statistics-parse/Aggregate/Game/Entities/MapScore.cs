using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities
{
    public class MapScore
    {
        public string Map { get; }

        public Score Score { get; }

        public MapScore(string map, Score score)
        {
            Map = map.NotNull(nameof(map));
            Score = score.NotNull(nameof(score));
        }
    }
}