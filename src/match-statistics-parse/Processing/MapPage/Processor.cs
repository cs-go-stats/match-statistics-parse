using System;
using System.Threading.Tasks;
using CSGOStats.Extensions.Validation;
using CSGOStats.Infrastructure.DataAccess.Contexts.EF;
using CSGOStats.Infrastructure.DataAccess.Repositories;
using CSGOStats.Infrastructure.PageParse.Page.Loading;
using CSGOStats.Infrastructure.PageParse.Page.Parsing;
using CSGOStats.Services.Core.Handling.Storage;
using CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities;
using CSGOStats.Services.MatchStatisticsParse.Data.Entities;
using Microsoft.EntityFrameworkCore;
using MapPageModel= CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.MapPage;

namespace CSGOStats.Services.MatchStatisticsParse.Processing.MapPage
{
    public class Processor
    {
        private readonly IPageParser<MapPageModel> _mapPageParser;
        private readonly Upsert<Game, Guid> _gameUpsert;
        private readonly IRepository<ScheduledMapParse> _scheduledMapRepository;
        private readonly BaseDataContext _context;

        public Processor(
            IPageParser<MapPageModel> mapPageParser,
            Upsert<Game, Guid> gameUpsert,
            IRepository<ScheduledMapParse> scheduledMapRepository,
            BaseDataContext context)
        {
            _mapPageParser = mapPageParser.NotNull(nameof(mapPageParser));
            _gameUpsert = gameUpsert.NotNull(nameof(gameUpsert));
            _scheduledMapRepository = scheduledMapRepository.NotNull(nameof(scheduledMapRepository));
            _context = context.NotNull(nameof(context));
        }

        public async Task RunAsync()
        {
            var map = await FindFirstMapAsync();
            if (map == null)
            {
                return;
            }

            var mapStatistics = await _mapPageParser.ParseAsync(new HttpContentLoader(map.Link));
            await _gameUpsert.Async(
                key: map.GameId,
                updater: game => game.OnMapPageParsedUpdate(mapStatistics));

            var scheduledEntity = await _scheduledMapRepository.GetAsync(map.Id);
            scheduledEntity.Processed();
            await _scheduledMapRepository.UpdateAsync(scheduledEntity.Id, scheduledEntity);
        }

        private Task<ScheduledMapParse> FindFirstMapAsync() =>
            _context.Set<ScheduledMapParse>().FirstOrDefaultAsync(x => !x.IsProcessed);
    }
}