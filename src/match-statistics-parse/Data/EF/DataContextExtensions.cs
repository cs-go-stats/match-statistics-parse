using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.DataAccess.Contexts.EF;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Services.MatchStatisticsParse.Data.EF
{
    public static class DataContextExtensions
    {
        public static Task EnsureDatabaseCreated(this IServiceProvider services) =>
            services.GetService<BaseDataContext>().Database.EnsureCreatedAsync();
    }
}