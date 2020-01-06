using System;
using System.Threading.Tasks;
using CSGOStats.Services.Core.Scheduling;
using CSGOStats.Services.MatchStatisticsParse.Processing.MapPage;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Services.MatchStatisticsParse.Scheduling
{
    public class MapPageStatisticsParseJob : JobBase
    {
        protected override string Code => nameof(MapPageStatisticsParseJob);

        protected override Task ExecuteAsync(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<Processor>().RunAsync();
    }
}