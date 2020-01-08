using CSGOStats.Extensions.Validation;

namespace CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities
{
    public class Performance
    {
        public string Team { get; }

        public string Player { get; }

        public Statistics Statistics { get; }

        public string Value { get; }

        public Performance(string team, string player, Statistics statistics, string value)
        {
            Team = team.NotNull(nameof(team));
            Player = player.NotNull(nameof(player));
            Statistics = statistics.NotNull(nameof(statistics));
            Value = value.NotNull(nameof(value));
        }
    }
}