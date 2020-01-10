using CSGOStats.Infrastructure.PageParse.Extraction;
using CSGOStats.Infrastructure.PageParse.Page.Parsing;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Mapping;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.Stats.Teams
{
    [ModelLeaf]
    public class StatisticsValue
    {
        [StatisticsCodeValue]
        public string Code { get; private set; }

        [PlainTextValue]
        public string Value { get; private set; }
    }
}