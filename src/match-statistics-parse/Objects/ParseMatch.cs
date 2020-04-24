using System;
using CSGOStats.Infrastructure.Core.Communication.Payload;
using CSGOStats.Infrastructure.Core.Validation;
using CSGOStats.Services.MatchStatisticsParse.Scheduling;

namespace CSGOStats.Services.MatchStatisticsParse.Objects
{
    [Delay(5)]
    public class ParseMatch : IMessage
    {
        public Guid GameId { get; }

        public ParseMatch(Guid gameId)
        {
            GameId = gameId.AnythingBut(Guid.Empty, nameof(gameId));
        }
    }
}