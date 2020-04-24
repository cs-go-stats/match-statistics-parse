using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.Mongo;

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