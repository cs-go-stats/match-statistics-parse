using CSGOStats.Infrastructure.PageParse.Page.Parsing;
using CSGOStats.Infrastructure.PageParse.Structure.Containers;
using CSGOStats.Infrastructure.PageParse.Structure.Markers;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Mapping.Veto;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Model.Header;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Model.Maps;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Model.Veto;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Model
{
    [ModelRoot, RootContainer("*/body/div[@class = 'bgPadding']/div/div[@class = 'colCon']/div[@class = 'contentCol']/div[@class = 'match-page']")]
    public class MatchPage
    {
        [RequiredContainer("div[contains(@class, 'teamsBox')]")]
        public HeaderModel Header { get; private set; }

        [Collection, OptionalContainer("div[contains(@class, 'maps')]/div[1]/div[contains(@class, 'veto-box')][2]/div/div"), VetoValue]
        public WrappedCollection<MapVeto> Veto { get; } = new WrappedCollection<MapVeto>();

        [Collection, RequiredContainer("div[contains(@class, 'maps')]/div[1]/div[contains(@class, 'flexbox-column')]/div")]
        public WrappedCollection<StatisticsLink> StatisticsLinks { get; } = new WrappedCollection<StatisticsLink>();
    }
}