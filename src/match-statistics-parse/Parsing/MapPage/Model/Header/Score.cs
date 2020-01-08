using CSGOStats.Extensions.Validation;

namespace CSGOStats.Services.MatchStatisticsParse.Parsing.MapPage.Model.Header
{
    public class Score
    {
        public int Team1 { get; }

        public int Team2 { get; }

        public Score(int team1, int team2)
        {
            Team1 = team1.Natural(nameof(team1));
            Team2 = team2.Natural(nameof(team2));
        }
    }
}