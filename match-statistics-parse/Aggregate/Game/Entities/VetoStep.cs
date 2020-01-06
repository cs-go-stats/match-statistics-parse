using CSGOStats.Extensions.Validation;

namespace CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities
{
    public class VetoStep
    {
        public int Order { get; }

        public string Team { get; }

        public string Decision { get; }

        public VetoStep(int order, string team, string decision)
        {
            Order = order.Natural(nameof(order));
            Team = team;
            Decision = decision.NotNull(nameof(decision));
        }

        public VetoStep(int order, string decision)
            : this(order, team: null, decision)
        {
        }
    }
}