using CSGOStats.Infrastructure.Core.Communication.Payload;
using CSGOStats.Infrastructure.Core.Validation;
using NodaTime;

namespace CSGOStats.Services.MatchStatisticsParse.Scheduling
{
    public class DelayedDeliverState
    {
        public Duration Delay { get; }

        public IMessage Payload { get; }

        public DelayedDeliverState(Duration delay, IMessage payload)
        {
            Delay = delay;
            Payload = payload.NotNull(nameof(payload));
        }
    }
}