using System;
using System.Linq;
using System.Threading.Tasks;
using CSGOStats.Extensions.Extensions;
using CSGOStats.Extensions.Validation;
using CSGOStats.Infrastructure.DataAccess.Contexts.EF;
using CSGOStats.Infrastructure.DataAccess.Repositories;
using CSGOStats.Infrastructure.PageParse.Page.Loading;
using CSGOStats.Infrastructure.PageParse.Page.Parsing;
using CSGOStats.Services.Core.Handling.Storage;
using CSGOStats.Services.Core.Links;
using CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities;
using CSGOStats.Services.MatchStatisticsParse.Data.Entities;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Model;
using Microsoft.EntityFrameworkCore;

namespace CSGOStats.Services.MatchStatisticsParse.Processing.GamePage
{
    public class Processor
    {
        private readonly IPageParser<MatchPage> _matchPageParser;
        private readonly Upsert<Game, Guid> _gameUpsert;
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<ScheduledGameParse> _scheduledGameRepository;
        private readonly IRepository<ScheduledMapParse> _scheduledMapRepository;
        private readonly BaseDataContext _context;

        public Processor(
            IPageParser<MatchPage> matchPageParser, 
            Upsert<Game, Guid> gameUpsert,
            IRepository<Game> gameRepository,
            IRepository<ScheduledGameParse> scheduledGameRepository,
            IRepository<ScheduledMapParse> scheduledMapRepository, 
            BaseDataContext context)
        {
            _matchPageParser = matchPageParser.NotNull(nameof(matchPageParser));
            _gameUpsert = gameUpsert.NotNull(nameof(gameUpsert));
            _gameRepository = gameRepository.NotNull(nameof(gameRepository));
            _scheduledGameRepository = scheduledGameRepository.NotNull(nameof(scheduledGameRepository));
            _scheduledMapRepository = scheduledMapRepository.NotNull(nameof(scheduledMapRepository));
            _context = context.NotNull(nameof(context));
        }

        public async Task RunAsync()
        {
            var game = await FindFirstGameAsync();
            if (game?.Link == null)
            {
                return;
            }

            var matchPage = await _matchPageParser.ParseAsync(new HttpContentLoader(game.Link));
            await _gameUpsert.Async(
                key: game.Id,
                updater: x => x.OnMatchPageParsedUpdate(matchPage));

            foreach (var statistics in matchPage.StatisticsLinks.Where(x => x.IsValid()))
            {
                var link = statistics.Link.HltvUri();
                var entity = new ScheduledMapParse(
                    id: link.Guid(),
                    gameId: game.Id,
                    link: link,
                    isProcessed: false,
                    map: statistics.Map);
                await _scheduledMapRepository.AddAsync(entity.Id, entity);
            }

            var scheduledEntity = await _scheduledGameRepository.GetAsync(game.Id);
            scheduledEntity.Processed();
            await _scheduledGameRepository.UpdateAsync(scheduledEntity.Id, scheduledEntity);
        }

        private async Task<Game> FindFirstGameAsync()
        {
            var scheduledParse = await _context.Set<ScheduledGameParse>().FirstOrDefaultAsync(x => !x.IsProcessed);
            return scheduledParse == null ? null : await _gameRepository.GetAsync(scheduledParse.Id);
        }
    }
}