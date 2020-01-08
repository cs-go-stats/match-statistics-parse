using CSGOStats.Extensions.Validation;

namespace CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities
{
    public class Score
    {
        public TeamResult Team1 { get; }

        public TeamResult Team2 { get; }

        public Score(TeamResult team1, TeamResult team2)
        {
            Team1 = team1.NotNull(nameof(team1));
            Team2 = team2.NotNull(nameof(team2));
        }
    }
}