using CSGOStats.Infrastructure.Core.PageParse.Extraction;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;
using CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Containers;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.Header
{
    [ModelLeaf]
    public class TeamComposition
    {
        [RequiredContainer("div[@class = 'team-left']/img"), ElementTitleValue]
        public string Team1 { get; private set; }

        [RequiredContainer("div[@class = 'team-right']/img"), ElementTitleValue]
        public string Team2 { get; private set; }
    }
}