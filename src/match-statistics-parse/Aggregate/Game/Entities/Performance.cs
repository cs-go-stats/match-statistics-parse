using System;
using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities
{
    public class Performance
    {
        public string Team { get; }

        public string Player { get; }

        public Statistics Statistics { get; }

        public string Value { get; }

        public string Map { get; }

        [Obsolete("Statistics without map specification doesn't make sense. Leaving it until Mongo storage will become consistent.")]
        public Performance(string team, string player, Statistics statistics, string value)
            : this(team: team, player: player, statistics: statistics, value: value, map: null)
        {
        }

        public Performance(string team, string player, Statistics statistics, string value, string map)
        {
            Team = team.NotNull(nameof(team));
            Player = player.NotNull(nameof(player));
            Statistics = statistics.NotNull(nameof(statistics));
            Value = value.NotNull(nameof(value));
            Map = map.NotNull(nameof(map));
        }
    }
}