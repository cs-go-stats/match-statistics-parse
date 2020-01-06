using System.Text.RegularExpressions;
using CSGOStats.Extensions.Validation;
using CSGOStats.Infrastructure.PageParse.Mapping;
using HtmlAgilityPack;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Mapping
{
    internal class StatisticsCodeMapper : IValueMapper
    {
        internal const string Code = nameof(StatisticsCodeMapper);

        public object Map(HtmlNode root) =>
            new Regex(@"st-[a-z]+")
                .Match(root.GetAttributeValue("class", null))
                .ForSucceeded()
                .Groups["code"].Value;
    }
}