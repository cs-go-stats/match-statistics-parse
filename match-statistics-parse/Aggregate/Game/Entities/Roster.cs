using System.Collections.Generic;
using CSGOStats.Extensions.Validation;

namespace CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities
{
    public class Roster
    {
        public string Tag { get; }

        public IReadOnlyCollection<Player> Players { get; }

        public Roster(string tag, IReadOnlyCollection<Player> players)
        {
            Tag = tag.NotNull(nameof(tag));
            Players = players.NotNull(nameof(players));
        }
    }
}