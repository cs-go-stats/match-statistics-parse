using CSGOStats.Infrastructure.DataAccess.Contexts.Mongo;

namespace CSGOStats.Services.MatchStatisticsParse.Data.Mongo
{
    public class MatchStatisticsContext : BaseMongoContext
    {
        static MatchStatisticsContext() => DocumentMapping.Registration();

        public MatchStatisticsContext(MongoDbConnectionSetting connectionSetting) 
            : base(connectionSetting)
        {
        }
    }
}