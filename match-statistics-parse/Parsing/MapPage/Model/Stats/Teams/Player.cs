using CSGOStats.Infrastructure.PageParse.Extraction;
using CSGOStats.Infrastructure.PageParse.Page.Parsing;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.Stats.Teams
{
    [ModelLeaf]
    public class Player
    {
        [PlainTextValue]
        public string Nickname { get; private set; }

        [AnchorLinkValue]
        public string Link { get; private set; }
    }
}