using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.PageParse.Mapping.Standard;
using HtmlAgilityPack;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Mapping.Header
{
    public class EventDateTimeValueMapper : ElementAttributeMapper
    {
        internal const string EventDateTimeValueCode = nameof(EventDateTimeValueMapper);

        public EventDateTimeValueMapper()
            : base("data-unix")
        {
        }

        public override object Map(HtmlNode root) =>
            base.Map(root).OfType<string>().Long().FromUnixTimestamp();
    }
}