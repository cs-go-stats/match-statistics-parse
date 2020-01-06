using CSGOStats.Extensions.Validation;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.Header
{
    public class ScoreBreakdown
    {
        public Score Overall { get; }

        public Score Half1 { get; }

        public Score Half2 { get; }

        public Score Overtime { get; }

        private ScoreBreakdown(Score overall, Score half1, Score half2, Score overtime)
        {
            Overall = overall.NotNull(nameof(overall));
            Half1 = half1.NotNull(nameof(half1));
            Half2 = half2.NotNull(nameof(half2));
            Overtime = overtime;
        }

        public static ScoreBreakdown Default(Score overall, Score half1, Score half2) =>
            new ScoreBreakdown(
                overall: overall,
                half1: half1,
                half2: half2,
                overtime: null);

        public static ScoreBreakdown Overtimes(Score overall, Score half1, Score half2, Score overtime) =>
            new ScoreBreakdown(
                overall: overall,
                half1: half1,
                half2: half2,
                overtime: overtime);
    }
}