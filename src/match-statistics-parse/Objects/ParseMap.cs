using System;
using CSGOStats.Infrastructure.Core.Communication.Payload;
using CSGOStats.Infrastructure.Core.Validation;
using CSGOStats.Services.MatchStatisticsParse.Scheduling;

namespace CSGOStats.Services.MatchStatisticsParse.Objects
{
    [Delay(5)]
    public class ParseMap : IMessage
    {
        public Guid GameId { get; }

        public string Link { get; }

        public string Map { get; private set; }

        public ParseMap(Guid gameId, string link, string map)
        {
            GameId = gameId.AnythingBut(Guid.Empty, nameof(gameId));
            Link = link.NotNull(nameof(link));
            Map = map.NotNull(nameof(map));
        }
    }
}