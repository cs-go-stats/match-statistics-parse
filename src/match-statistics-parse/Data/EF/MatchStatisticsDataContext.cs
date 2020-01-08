using CSGOStats.Infrastructure.DataAccess.Contexts.EF;
using Microsoft.EntityFrameworkCore;

namespace CSGOStats.Services.MatchStatisticsParse.Data.EF
{
    public class MatchStatisticsDataContext : BaseDataContext
    {
        public MatchStatisticsDataContext(PostgreConnectionSettings settings) 
            : base(settings)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MatchStatisticsDataContext).Assembly);
        }
    }

    // todo
    public class MigrationsDataContext : MatchStatisticsDataContext
    {
        public MigrationsDataContext()
            : base(new PostgreConnectionSettings(
                host: "localhost",
                database: "match-statistics-parse",
                username: "postgres",
                password: "dotFive1",
                isAuditEnabled: false))
        {
        }
    }
}