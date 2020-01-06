using CSGOStats.Extensions.Validation;

namespace CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities
{
    public class Player
    {
        public string Nickname { get; }

        public string Link { get; }

        public Player(string nickname, string link)
        {
            Nickname = nickname.NotNull(nameof(nickname));
            Link = link.NotNull(nameof(link));
        }
    }
}