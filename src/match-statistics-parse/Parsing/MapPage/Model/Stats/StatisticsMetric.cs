using CSGOStats.Infrastructure.PageParse.Extraction;
using CSGOStats.Infrastructure.PageParse.Page.Parsing;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Mapping;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.Stats
{
    [ModelLeaf]
    public class StatisticsMetric
    {
        [StatisticsCodeValue]
        public string Code { get; private set; }

        [PlainTextValue]
        public string Title { get; private set; }

        [ElementTitleValue]
        public string Description { get; private set; }
    }
}