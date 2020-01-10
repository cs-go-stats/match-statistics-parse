using CSGOStats.Extensions.Validation;

namespace CSGOStats.Services.MatchStatisticsParse.Aggregate.Game.Entities
{
    public class Statistics
    {
        public string Title { get; }

        public string Description { get; }

        public Statistics(string title, string description)
        {
            Title = title.NotNull(nameof(title));
            Description = description;
        }

        public Statistics(string title)
            : this(title, null)
        {
        }
    }
}