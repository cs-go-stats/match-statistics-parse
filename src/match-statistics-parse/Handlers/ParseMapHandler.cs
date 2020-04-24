using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Handling;
using CSGOStats.Infrastructure.Core.Context;
using CSGOStats.Infrastructure.Core.Data.Storage;
using CSGOStats.Infrastructure.Core.PageParse.Page.Load;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;
using CSGOStats.Infrastructure.Core.Validation;
using CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities;
using CSGOStats.Services.MatchStatisticsParse.Objects;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model;

namespace CSGOStats.Services.MatchStatisticsParse.Handlers
{
    public class ParseMapHandler : BaseMessageHandler<ParseMap>
    {
        private readonly IPageParser<MapPage> _mapPageParser;
        private readonly Upsert<Game, Guid> _gameUpsert;

        public ParseMapHandler(
            ExecutionContext context, 
            IPageParser<MapPage> mapPageParser, 
            Upsert<Game, Guid> gameUpsert) 
                : base(context)
        {
            _mapPageParser = mapPageParser.NotNull(nameof(mapPageParser));
            _gameUpsert = gameUpsert.NotNull(nameof(gameUpsert));
        }

        public override async Task HandleAsync(ParseMap message)
        {
            var mapPage = await _mapPageParser.ParseAsync(new HttpContentLoader(message.Link));
            await _gameUpsert.Async(message.GameId, game => game.OnMapPageParsedUpdate(mapPage, message.Map));
        }
    }
}