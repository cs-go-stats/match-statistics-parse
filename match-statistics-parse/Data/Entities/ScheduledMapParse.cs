using System;
using CSGOStats.Extensions.Validation;
using CSGOStats.Infrastructure.DataAccess.Entities;

namespace CSGOStats.Services.MatchStatisticsParse.Data.Entities
{
    public class ScheduledMapParse : IHaveIdEntity
    {
        public Guid Id { get; }

        public Guid GameId { get; }

        public string Link { get; }

        public bool IsProcessed { get; private set; }

        public string Map { get; private set; }

        public ScheduledMapParse(Guid id, Guid gameId, string link, bool isProcessed, string map)
        {
            Id = id.AnythingBut(Guid.Empty, nameof(id));
            GameId = gameId.AnythingBut(Guid.Empty, nameof(gameId));
            Link = link.NotNull(nameof(link));
            IsProcessed = isProcessed;
            Map = map.NotNull(nameof(map));
        }

        internal void Processed()
        {
            IsProcessed = true;
        }
    }
}