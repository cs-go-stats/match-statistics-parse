using System;
using System.Threading.Tasks;
using CSGOStats.Extensions.Extensions;
using CSGOStats.Extensions.Validation;
using CSGOStats.Infrastructure.DataAccess.Repositories;
using CSGOStats.Infrastructure.Messaging.Handling;
using CSGOStats.Services.Core.Handling.Storage;
using CSGOStats.Services.HistoryParse.Objects;
using CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities;
using CSGOStats.Services.MatchStatisticsParse.Data.Entities;

namespace CSGOStats.Services.MatchStatisticsParse.Handlers
{
    public class HistoricalMatchHandler : BaseMessageHandler<HistoricalMatchParsed>
    {
        private readonly Upsert<Game, Guid> _gameUpsert;
        private readonly IRepository<ScheduledGameParse> _scheduleGameParseRepository;

        public HistoricalMatchHandler(
            Upsert<Game, Guid> gameUpsert,
            IRepository<ScheduledGameParse> scheduleGameParseRepository)
        {
            _gameUpsert = gameUpsert.NotNull(nameof(gameUpsert));
            _scheduleGameParseRepository = scheduleGameParseRepository.NotNull(nameof(scheduleGameParseRepository));
        }

        public override async Task HandleAsync(HistoricalMatchParsed message)
        {
            var game = await _gameUpsert.Async(
                key: message.Link.Guid(),
                updater: x => x.OnHistoricalMatchParsedUpdate(message));

            await ScheduledParseAsync(game);
        }

        private async Task ScheduledParseAsync(Game game)
        {
            var scheduleEntity = await _scheduleGameParseRepository.FindAsync(game.Id);
            if (scheduleEntity != null)
            {
                return;
            }

            await _scheduleGameParseRepository.AddAsync(
                id: game.Id,
                entity: new ScheduledGameParse(
                    id: game.Id,
                    link: game.Link,
                    isProcessed: false));
        }
    }
}