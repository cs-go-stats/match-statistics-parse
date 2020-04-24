using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities
{
    public class VetoedMap
    {
        public string Name { get; }

        public VetoStep Veto { get; }

        public VetoedMap(string name, VetoStep veto)
        {
            Name = name.NotNull(nameof(name));
            Veto = veto.NotNull(nameof(veto));
        }
    }
}