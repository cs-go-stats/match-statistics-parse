using System.Text.RegularExpressions;
using CSGOStats.Infrastructure.Core.PageParse.Mapping;
using CSGOStats.Infrastructure.Core.Validation;
using HtmlAgilityPack;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Mapping
{
    internal class StatisticsCodeMapper : IValueMapper
    {
        internal const string Code = nameof(StatisticsCodeMapper);

        public object Map(HtmlNode root) =>
            new Regex(@"(?<code>st-[a-z]+)")
                .Match(root.GetAttributeValue("class", null))
                .ForSucceeded()
                .Groups["code"].Value;
    }
}