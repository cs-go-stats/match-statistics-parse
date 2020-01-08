using System;
using CSGOStats.Infrastructure.Messaging.Handling;
using CSGOStats.Services.HistoryParse.Objects;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Services.MatchStatisticsParse.Handlers
{
    public static class HandlersExtensions
    {
        public static void SubscribeForMessages(this IServiceProvider services)
        {
            RegisterHandlers(services.GetService<IMessageRegistrar>());
        }

        private static void RegisterHandlers(IMessageRegistrar registrar)
        {
            registrar.RegisterForType(typeof(HistoricalMatchParsed));
        }
    }
}