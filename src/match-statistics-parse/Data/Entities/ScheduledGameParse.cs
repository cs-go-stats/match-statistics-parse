using System;
using CSGOStats.Extensions.Validation;
using CSGOStats.Infrastructure.DataAccess.Entities;

namespace CSGOStats.Services.MatchStatisticsParse.Data.Entities
{
    public class ScheduledGameParse : IHaveIdEntity
    {
        public Guid Id { get; }

        public string Link { get; }

        public bool IsProcessed { get; private set; }

        public ScheduledGameParse(Guid id, string link, bool isProcessed)
        {
            Id = id.AnythingBut(Guid.Empty, nameof(id));
            Link = link.NotNull(nameof(link));
            IsProcessed = isProcessed;
        }

        internal void Processed()
        {
            IsProcessed = true;
        }
    }
}