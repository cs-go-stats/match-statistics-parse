using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Data.Entities;
using CSGOStats.Infrastructure.Core.Data.Storage;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.Initialization;
using CSGOStats.Infrastructure.Core.PageParse.Page.Parse;
using CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities;
using CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Factories;
using CSGOStats.Services.MatchStatisticsParse.Data.EF;
using CSGOStats.Services.MatchStatisticsParse.Data.Mongo;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Model;
using CSGOStats.Services.MatchStatisticsParse.Runtime;
using CSGOStats.Services.MatchStatisticsParse.Scheduling;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CSGOStats.Services.MatchStatisticsParse
{
    internal static class Program
    {
        private static async Task Main()
        {
#if DEBUG
            var environment = Environments.Development;
#else
            var environment = Environments.Production;
#endif
            await Startup
                .ForEnvironment(Service.Name, environment)
                .WithMessaging<Service>()
                .UsesPostgres<MatchStatisticsDataContext>()
                .UsesMongo<MatchStatisticsContext>()
                .ConfigureServices(ConfigureServiceProvider)
                .RunAsync(new MatchStatisticsParseRuntimeAction());
        }

        private static IServiceCollection ConfigureServiceProvider(
            IServiceCollection services,
            IConfigurationRoot configuration) =>
                services
                    .AddRepositories()
                    .AddUpserts()
                    .AddProcessors()
                    .AddScheduling();

        private static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .RegisterMongoRepositoryFor<Game>(isGuidRepository: true);

        private static IServiceCollection AddUpserts(this IServiceCollection services) =>
            services
                .AddScoped<Upsert<Game, Guid>>()
                .AddScoped<IEntityFactory<Game, Guid>, GameFactory>();

        private static IServiceCollection AddProcessors(this IServiceCollection services) =>
            services
                .AddScoped<IPageParser<MatchPage>, MatchPageParser>()
                .AddScoped<IPageParser<MapPage>, MapPageParser>();

        private static IServiceCollection AddScheduling(this IServiceCollection services) =>
            services.AddSingleton<IDelayState>(s => new GlobalDelayState(s));
    }
}
