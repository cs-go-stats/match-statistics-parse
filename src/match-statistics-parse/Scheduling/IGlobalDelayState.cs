using System.Threading.Tasks;

namespace CSGOStats.Services.MatchStatisticsParse.Scheduling
{
    public interface IDelayState
    {
        Task SchedulePublishAsync(DelayedDeliverState state);
    }
}