using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities
{
    public class TeamResult
    {
        public string Name { get; }

        public int Total { get; }

        public int Half1 { get; }

        public int Half2 { get; }

        public int Overtime { get; }

        public TeamResult(string name, int total, int half1, int half2, int overtime)
        {
            Name = name.NotNull(nameof(name));
            Total = total.Natural(nameof(total));
            Half1 = half1.Natural(nameof(half1));
            Half2 = half2.Natural(nameof(half2));
            Overtime = overtime.Natural(nameof(overtime));
        }
    }
}