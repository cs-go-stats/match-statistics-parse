using System;
using System.Linq;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Handling;
using CSGOStats.Infrastructure.Core.Context;
using CSGOStats.Infrastructure.Core.Data.Storage;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.PageParse.Page.Load;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;
using CSGOStats.Infrastructure.Core.Validation;
using CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities;
using CSGOStats.Services.MatchStatisticsParse.Objects;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Model;
using CSGOStats.Services.MatchStatisticsParse.Scheduling;

namespace CSGOStats.Services.MatchStatisticsParse.Handlers
{
    public class ParseMatchHandler : BaseMessageHandler<ParseMatch>
    {
        private readonly Upsert<Game, Guid> _gameUpsert;
        private readonly IPageParser<MatchPage> _matchPageParser;
        private readonly IDelayState _delayState;

        public ParseMatchHandler(
            ExecutionContext context, 
            Upsert<Game, Guid> gameUpsert, 
            IPageParser<MatchPage> matchPageParser, 
            IDelayState delayState)
                : base(context)
        {
            _gameUpsert = gameUpsert.NotNull(nameof(gameUpsert));
            _matchPageParser = matchPageParser.NotNull(nameof(matchPageParser));
            _delayState = delayState.NotNull(nameof(delayState));
        }

        public override Task HandleAsync(ParseMatch message) =>
            _gameUpsert.Async(message.GameId, ParseMatchAsync);

        private async Task ParseMatchAsync(Game game)
        {
            var matchPage = await _matchPageParser.ParseAsync(new HttpContentLoader(game.Link));
            game.OnMatchPageParsedUpdate(matchPage);

            foreach (var statistics in matchPage.StatisticsLinks.Where(x => x.IsValid()))
            {
                await _delayState.SchedulePublishAsync(new ParseMap(game.Id, statistics.Link.HltvUri(), statistics.Map).ToDeliverState());
            }
        }
    }
}