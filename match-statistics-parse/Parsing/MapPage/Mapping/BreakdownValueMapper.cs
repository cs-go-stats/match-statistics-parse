using System;
using System.Text.RegularExpressions;
using CSGOStats.Extensions.Extensions;
using CSGOStats.Infrastructure.PageParse.Mapping;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.Header;
using HtmlAgilityPack;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Mapping
{
    internal class BreakdownValueMapper : IValueMapper
    {
        public const string BreakdownValueCode = nameof(BreakdownValueMapper);

        public object Map(HtmlNode root)
        {
            var text = root.InnerText;
            return TryParseOvertimes(text) ??
                   TryParseDefaultBreakdown(text) ??
                   throw new CantParseScoreBreakdown(text);
        }

        private static ScoreBreakdown TryParseOvertimes(string text) =>
            TryGetFromRegex(
                regex: new Regex(@"(?<overall_team1>\d{1,2})\s:\s(?<overall_team2>\d{1,2})\s\(\s(?<half1_team1>\d{1,2})\s:\s(?<half1_team2>\d{1,2})\s\)\s\(\s(?<half2_team1>\d{1,2})\s:\s(?<half2_team2>\d{1,2})\s\)\s\(\s(?<overtime_team1>\d{1,2})\s:\s(?<overtime_team2>\d{1,2})\s\)"),
                text: text,
                creator: match => ScoreBreakdown.Overtimes(
                    overall: new Score(
                        team1: match.Groups["overall_team1"].Value.Int(),
                        team2: match.Groups["overall_team2"].Value.Int()),
                    half1: new Score(
                        team1: match.Groups["half1_team1"].Value.Int(),
                        team2: match.Groups["half1_team2"].Value.Int()),
                    half2: new Score(
                        team1: match.Groups["half2_team1"].Value.Int(),
                        team2: match.Groups["half2_team2"].Value.Int()),
                    overtime: new Score(
                        team1: match.Groups["overtime_team1"].Value.Int(),
                        team2: match.Groups["overtime_team2"].Value.Int())));

        private static ScoreBreakdown TryParseDefaultBreakdown(string text) =>
            TryGetFromRegex(
                regex: new Regex(@"(?<overall_team1>\d{1,2})\s:\s(?<overall_team2>\d{1,2})\s\(\s(?<half1_team1>\d{1,2})\s:\s(?<half1_team2>\d{1,2})\s\)\s\(\s(?<half2_team1>\d{1,2})\s:\s(?<half2_team2>\d{1,2})\s\)"),
                text: text,
                creator: match => ScoreBreakdown.Default(
                    overall: new Score(
                        team1: match.Groups["overall_team1"].Value.Int(),
                        team2: match.Groups["overall_team2"].Value.Int()),
                    half1: new Score(
                        team1: match.Groups["half1_team1"].Value.Int(),
                        team2: match.Groups["half1_team2"].Value.Int()),
                    half2: new Score(
                        team1: match.Groups["half2_team1"].Value.Int(),
                        team2: match.Groups["half2_team2"].Value.Int())));

        private static ScoreBreakdown TryGetFromRegex(Regex regex, string text, Func<Match, ScoreBreakdown> creator)
        {
            var match = regex.Match(text);
            return match.Success ? creator(match) : null;
        }
    }
}