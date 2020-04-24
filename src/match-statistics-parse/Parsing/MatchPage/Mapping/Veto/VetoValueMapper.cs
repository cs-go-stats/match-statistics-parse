using System;
using System.Text.RegularExpressions;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.PageParse.Mapping;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Model.Veto;
using HtmlAgilityPack;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Mapping.Veto
{
    public class VetoValueMapper : IValueMapper
    {
        internal const string VetoMapperCode = nameof(VetoValueMapper);

        public object Map(HtmlNode root)
        {
            var text = root.InnerText;
            return TryExtractPickVeto(text) ??
                   TryExtractBanVeto(text) ??
                   TryExtractLeftover(text) ??
                   TryExtractRandomSelection(text) ??
                   throw new CantDefineMapVeto(text);
        }

        private static MapVeto TryExtractPickVeto(string text) =>
            TryExtractMapVeto(
                regex: new Regex(@"(?<no>\d)\.\s(?<team>.+)\sremoved\s(?<map>[A-Za-z0-9]+)"),
                text: text,
                builder: match => new MapVeto(
                    order: match.Groups["no"].Value.Int(),
                    teamCode: match.Groups["team"].Value,
                    mapCode: match.Groups["map"].Value,
                    decision: MapVeto.Desicion.Picked));

        private static MapVeto TryExtractBanVeto(string text) =>
            TryExtractMapVeto(
                regex: new Regex(@"(?<no>\d)\.\s(?<team>.+)\spicked\s(?<map>[A-Za-z0-9]+)"),
                text: text,
                builder: match => new MapVeto(
                    order: match.Groups["no"].Value.Int(),
                    teamCode: match.Groups["team"].Value,
                    mapCode: match.Groups["map"].Value,
                    decision: MapVeto.Desicion.Removed));

        private static MapVeto TryExtractLeftover(string text) =>
            TryExtractMapVeto(
                regex: new Regex(@"(?<no>\d)\.\s(?<map>[A-Za-z0-9]+)\swas\sleft\sover"),
                text: text,
                builder: match => new MapVeto(
                    order: match.Groups["no"].Value.Int(),
                    teamCode: null,
                    mapCode: match.Groups["map"].Value,
                    decision: MapVeto.Desicion.LeftOver));

        private static MapVeto TryExtractRandomSelection(string text) =>
            TryExtractMapVeto(
                regex: new Regex(@"(?<no>\d)\.\s(?<map>[A-Za-z0-9]+)\swas\srandomly\sselected\sof\sthe\sremaining\smaps\s\([A-Za-z0-9,\s]+\sleft\)"),
                text: text,
                builder: match => new MapVeto(
                    order: match.Groups["no"].Value.Int(),
                    teamCode: null,
                    mapCode: match.Groups["map"].Value,
                    decision: MapVeto.Desicion.SelectedRandomly));

        private static MapVeto TryExtractMapVeto(Regex regex, string text, Func<Match, MapVeto> builder)
        {
            var match = regex.Match(text);
            return match.Success ? builder.Invoke(match) : null;
        }
    }
}