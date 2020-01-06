using System;
using System.Threading.Tasks;
using CSGOStats.Services.Core.Scheduling;
using CSGOStats.Services.MatchStatisticsParse.Processing.GamePage;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Services.MatchStatisticsParse.Scheduling
{
    public class GamePageStatisticsParseJob : JobBase
    {
        protected override string Code => nameof(GamePageStatisticsParseJob);

        protected override Task ExecuteAsync(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<Processor>().RunAsync();
    }
}