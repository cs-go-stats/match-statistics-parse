using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.DataAccess;
using CSGOStats.Infrastructure.PageParse.Page.Parsing;
using CSGOStats.Services.Core.Handling.Entities;
using CSGOStats.Services.Core.Handling.Storage;
using CSGOStats.Services.Core.Initialization;
using CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities;
using CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Factories;
using CSGOStats.Services.MatchStatisticsParse.Data.EF;
using CSGOStats.Services.MatchStatisticsParse.Data.Entities;
using CSGOStats.Services.MatchStatisticsParse.Data.Mongo;
using CSGOStats.Services.MatchStatisticsParse.Handlers;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage;
using CSGOStats.Services.MatchStatisticsParse.Parsing.MatchPage.Model;
using CSGOStats.Services.MatchStatisticsParse.Scheduling;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GamePageProcessing = CSGOStats.Services.MatchStatisticsParse.Processing.GamePage;
using MapPageProcessing = CSGOStats.Services.MatchStatisticsParse.Processing.MapPage;

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
            var startupBuilder = await Startup
                .ForEnvironment(Service.Name, environment)
                .WithMessaging<Service>()
                .UsesPostgres()
                .UsesMongo()
                .WithJobsAsync(ScheduleExtensions.ConfigureJobs);
            await startupBuilder
                .ConfigureServices(ConfigureServiceProvider)
                .RunAsync(
                    actionBeforeStart: async services =>
                    {
                        await services.EnsureDatabaseCreated();
                        services.SubscribeForMessages();
                    });
        }

        private static IServiceCollection ConfigureServiceProvider(
            IServiceCollection services,
            IConfigurationRoot configuration) =>
                services
                    .ConfigureDatabase()
                    .AddUpserts()
                    .AddProcessors();

        private static IServiceCollection ConfigureDatabase(
            this IServiceCollection services) =>
                services
                    .RegisterMongoContext<MatchStatisticsContext>()
                    .RegisterPostgresContext<MatchStatisticsDataContext>()
                    .AddRepositories();

        private static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .RegisterPostgresRepositoryFor<ScheduledGameParse>()
                .RegisterPostgresRepositoryFor<ScheduledMapParse>()
                .RegisterMongoRepositoryFor<Game>(isGuidRepository: true);

        private static IServiceCollection AddUpserts(this IServiceCollection services) =>
            services
                .AddScoped<Upsert<Game, Guid>>()
                .AddScoped<IEntityFactory<Game, Guid>, GameFactory>();

        private static IServiceCollection AddProcessors(this IServiceCollection services) =>
            services
                .AddScoped<GamePageProcessing.Processor>()
                .AddScoped<MapPageProcessing.Processor>()
                .AddScoped<IPageParser<MatchPage>, MatchPageParser>()
                .AddScoped<IPageParser<MapPage>, MapPageParser>();
    }
}
