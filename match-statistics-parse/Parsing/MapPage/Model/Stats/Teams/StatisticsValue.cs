using CSGOStats.Infrastructure.PageParse.Extraction;
using CSGOStats.Infrastructure.PageParse.Page.Parsing;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.Stats.Teams
{
    [ModelLeaf]
    public class StatisticsValue
    {
        [ElementClassValue]
        public string Code { get; private set; }

        [PlainTextValue]
        public string Value { get; private set; }
    }
}