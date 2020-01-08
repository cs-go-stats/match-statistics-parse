using CSGOStats.Infrastructure.PageParse.Page.Parsing;
using CSGOStats.Infrastructure.PageParse.Structure.Containers;
using CSGOStats.Infrastructure.PageParse.Structure.Markers;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.Stats.Teams;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.Stats
{
    [ModelLeaf]
    public class TeamsStatistics
    {
        [Collection, RequiredContainer("table[@class = 'stats-table'][1]/thead/tr/th[not(contains(@class, 'st-teamname'))]")]
        public WrappedCollection<StatisticsMetric> Metrics { get; } = new WrappedCollection<StatisticsMetric>();

        [Collection, RequiredContainer("table[@class = 'stats-table'][1]/tbody/tr")]
        public WrappedCollection<PlayerStatistics> Team1Players { get; } = new WrappedCollection<PlayerStatistics>();

        [Collection, RequiredContainer("table[@class = 'stats-table'][2]/tbody/tr")]
        public WrappedCollection<PlayerStatistics> Team2Players { get; } = new WrappedCollection<PlayerStatistics>();
    }
}