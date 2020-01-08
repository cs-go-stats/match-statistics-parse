using System;
using CSGOStats.Services.Core.Handling.Entities;

namespace CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Factories
{
    public class GameFactory : IEntityFactory<Entities.Game, Guid>
    {
        private const long InitialVersion = 1L;

        public Entities.Game CreateEmpty(Guid id) => new Entities.Game(
            id: id,
            version: InitialVersion,
            link: null,
            dateTime: null,
            @event: null,
            rosters: null,
            maps: null,
            statistics: null,
            score: null);
    }
}