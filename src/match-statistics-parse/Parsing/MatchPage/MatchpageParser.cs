using CSGOStats.Infrastructure.Core.PageParse.Mapping;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;
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