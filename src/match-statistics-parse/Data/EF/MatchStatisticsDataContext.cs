using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF;

namespace CSGOStats.Services.MatchStatisticsParse.Data.EF
{
    public class MatchStatisticsDataContext : BaseDataContext
    {
        public MatchStatisticsDataContext(PostgreConnectionSettings settings) 
            : base(settings)
        {
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