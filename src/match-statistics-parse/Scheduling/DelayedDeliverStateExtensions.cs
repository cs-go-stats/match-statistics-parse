using System.Reflection;
using CSGOStats.Infrastructure.Core.Communication.Payload;
using CSGOStats.Infrastructure.Core.Validation;
using NodaTime;

namespace CSGOStats.Services.MatchStatisticsParse.Scheduling
{
    public static class DelayedDeliverStateExtensions
    {
        internal static DelayedDeliverState ToDeliverState(this IMessage message)
        {
            var delayInSeconds= message.NotNull(nameof(message)).GetType().GetCustomAttribute<DelayAttribute>(true)?.Seconds ;
            return new DelayedDeliverState(
                delay: delayInSeconds == null
                    ? Duration.Zero
                    : Duration.FromSeconds(delayInSeconds.Value),
                payload: message);
        }
    }
}