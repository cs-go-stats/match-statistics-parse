using CSGOStats.Infrastructure.Core.Initialization.RT.Actions;
using CSGOStats.Services.HistoryParse.Objects;

namespace CSGOStats.Services.MatchStatisticsParse.Runtime
{
    internal class MatchStatisticsParseRuntimeAction : ActionsAggregator
    {
        public MatchStatisticsParseRuntimeAction()
            : base(
                new CreateRelationalDatabaseAction(),
                new MigrateRelationalDatabaseAction(),
                new ExecuteJobsAction(),
                new RegisterMessageHandlerForTypesAction(typeof(HistoricalMatchParsed)),
                new WaitForExternalInterruptionAction())
        {
        }
    }
}