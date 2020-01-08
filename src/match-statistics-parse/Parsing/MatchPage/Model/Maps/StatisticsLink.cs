using CSGOStats.Infrastructure.PageParse.Extraction;
using CSGOStats.Infrastructure.PageParse.Page.Parsing;
using CSGOStats.Infrastructure.PageParse.Structure.Containers;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Model.Maps
{
    [ModelLeaf]
    public class StatisticsLink
    {
        [OptionalContainer("div[not(contains(@class, 'results'))]/div/div"), PlainTextValue]
        public string Map { get; private set; }

        [OptionalContainer("div[@class = 'results']/div[@class = 'results-center']/div[@class = 'results-center-stats']/a"), AnchorLinkValue]
        public string Link { get; private set; }

        internal bool IsValid() => Map != null && Link != null;
    }
}