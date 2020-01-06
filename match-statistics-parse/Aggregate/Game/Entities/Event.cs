using CSGOStats.Extensions.Validation;

namespace CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities
{
    public class Event
    {
        public string Name { get; }

        public string Link { get; }

        public Event(string name, string link)
        {
            Name = name.NotNull(nameof(name));
            Link = link.NotNull(nameof(link));
        }
    }
}