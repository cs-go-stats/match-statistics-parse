using System.Diagnostics;
using CSGOStats.Infrastructure.PageParse.Extraction;
using CSGOStats.Infrastructure.PageParse.Page.Parsing;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Model.Header
{
    [ModelLeaf]
    [DebuggerDisplay("Event = {Name}.")]
    public class Event
    {
        [ElementTitleValue]
        public string Name { get; private set; }

        [AnchorLinkValue]
        public string Link { get; private set; }
    }
}