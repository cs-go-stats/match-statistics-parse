using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Transport;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.Validation;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;

namespace CSGOStats.Services.MatchStatisticsParse.Scheduling
{
    internal class GlobalDelayState : IDelayState
    {
        private OffsetDateTime _bounds = DateTimeExtensions.GetCurrentDate;

        private readonly IServiceProvider _serviceProvider;

        public GlobalDelayState(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.NotNull(nameof(serviceProvider));
        }

        public async Task SchedulePublishAsync(DelayedDeliverState state)
        {
            UpdateBounds(state.Delay);

            using var scope = _serviceProvider.CreateScope();
            await scope.ServiceProvider.GetService<IEventBus>().ScheduleAsync(DateTimeExtensions.GetCurrentDate.Minus(_bounds), state.Payload);
        }

        private void UpdateBounds(Duration stateDelay)
        {
            var currentDate = DateTimeExtensions.GetCurrentDate;
            var newBound= _bounds.Plus(stateDelay);

            if (currentDate.Minus(newBound).TotalTicks > 0D)
            {
                _bounds = DateTimeExtensions.GetCurrentDate;
            }
            else
            {
                _bounds = newBound;
            }
        }
    }
}