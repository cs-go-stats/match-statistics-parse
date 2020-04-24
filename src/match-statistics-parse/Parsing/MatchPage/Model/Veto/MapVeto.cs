using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Model.Veto
{
    public class MapVeto
    {
        public int Order { get; }

        public string TeamCode { get; }

        public string MapCode { get; }

        public string Decision { get; }

        public MapVeto(int order, string teamCode, string mapCode, string decision)
        {
            Order = order.Natural(nameof(order));
            TeamCode = teamCode;
            MapCode = mapCode.NotNull(nameof(mapCode));
            Decision = decision.NotNull(nameof(decision));
        }

        public static class Desicion
        {
            public const string Picked = nameof(Picked);
            public const string Removed = nameof(Removed);
            public const string LeftOver = nameof(LeftOver);
            public const string SelectedRandomly = nameof(SelectedRandomly);
        }
    }
}