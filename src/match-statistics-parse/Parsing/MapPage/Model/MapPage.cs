using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;
using CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Containers;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Mapping;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.Header;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.Stats;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model
{
    [ModelRoot, RootContainer("*/body/div[@class = 'bgPadding']/div/div[@class = 'colCon']/div[@class = 'contentCol']")]
    public class MapPage
    {
        [RequiredContainer("div/div[@class = 'wide-grid']/div[contains(@class, 'match-info-box-col')]/div/div[@class = 'match-info-box']")]
        public TeamComposition Teams { get; private set; }

        [RequiredContainer("div/div[@class = 'wide-grid']/div[contains(@class, 'match-info-box-col')]/div/div[@class = 'match-info-row'][1]/div[@class = 'right']"), BreakdownValue]
        public ScoreBreakdown Breakdown { get; private set; }

        [RequiredContainer("div")]
        public TeamsStatistics Statistics { get; private set; }
    }
}