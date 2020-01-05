using CSGOStats.Infrastructure.PageParse.Page.Parsing;
using CSGOStats.Infrastructure.PageParse.Structure.Containers;
using CSGOStats.Infrastructure.PageParse.Structure.Markers;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.Stats.Teams
{
    [ModelLeaf]
    public class PlayerStatistics
    {
        [RequiredContainer("td[@class = 'st-player']/a"),]
        public Player Player { get; private set; }

        [Collection, RequiredContainer("td[not(@class = 'st-player')]")]
        public WrappedCollection<StatisticsValue> Statistics { get; } = new WrappedCollection<StatisticsValue>();
    }
}