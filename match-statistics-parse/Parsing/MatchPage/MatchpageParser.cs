using CSGOStats.Infrastructure.PageParse.Mapping;
using CSGOStats.Infrastructure.PageParse.Page.Parsing;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Mapping;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage
{
    public class MatchPageParser : PageParser<Model.MatchPage>
    {
        public MatchPageParser() 
            : base(new BaseDictionaryAdaptedValueMapperFactory(
                new MatchPageValueMapperFactory()))
        {
        }
    }
}