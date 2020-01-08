using CSGOStats.Infrastructure.PageParse.Mapping;
using CSGOStats.Infrastructure.PageParse.Page.Parsing;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Mapping;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage
{
    public class MapPageParser : PageParser<Model.MapPage>
    {
        public MapPageParser() 
            : base(new BaseDictionaryAdaptedValueMapperFactory(
                new MapPageValueMapperFactory()))
        {
        }
    }
}