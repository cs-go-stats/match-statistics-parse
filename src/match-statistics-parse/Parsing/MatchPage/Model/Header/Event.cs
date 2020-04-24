using System.Diagnostics;
using CSGOStats.Infrastructure.Core.PageParse.Extraction;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;

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