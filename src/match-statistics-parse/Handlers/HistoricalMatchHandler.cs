using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Handling;
using CSGOStats.Infrastructure.Core.Context;
using CSGOStats.Infrastructure.Core.Data.Storage;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.Validation;
using CSGOStats.Services.HistoryParse.Objects;
using CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities;
using CSGOStats.Services.MatchStatisticsParse.Objects;
using CSGOStats.Services.MatchStatisticsParse.Scheduling;

namespace CSGOStats.Services.MatchStatisticsParse.Handlers
{
    public class HistoricalMatchHandler : BaseMessageHandler<HistoricalMatchParsed>
    {
        private readonly Upsert<Game, Guid> _gameUpsert;
        private readonly IDelayState _delayState;

        public HistoricalMatchHandler(
            ExecutionContext executionContext,
            Upsert<Game, Guid> gameUpsert,
            IDelayState delayState)
            : base(executionContext)
        {
            _gameUpsert = gameUpsert.NotNull(nameof(gameUpsert));
            _delayState = delayState.NotNull(nameof(delayState));
        }

        public override async Task HandleAsync(HistoricalMatchParsed message)
        {
            var game = await _gameUpsert.Async(
                key: message.Link.Guid(),
                updater: x => x.OnHistoricalMatchParsedUpdate(message));

            await _delayState.SchedulePublishAsync(new ParseMatch(game.Id).ToDeliverState());
        }
    }
}