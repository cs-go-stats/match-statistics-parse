using System.Diagnostics;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;
using CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Containers;
using CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Markers;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Mapping.Header;
using NodaTime;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Model.Header
{
    [ModelLeaf]
    [DebuggerDisplay("Match time = {MatchTime}; Event = {Event.Name}.")]
    public class HeaderModel
    {
        [Collection, RequiredContainer("div[@class = 'team']/div")]
        public WrappedCollection<Team> Teams { get; } = new WrappedCollection<Team>();

        [RequiredContainer("div[@class = 'timeAndEvent']/div[@class = 'time']"), EventDateTimeValue]
        public OffsetDateTime MatchTime { get; private set; }

        [RequiredContainer("div[@class = 'timeAndEvent']/div[contains(@class, 'event')]/a")]
        public Event Event { get; private set; }
    }
}