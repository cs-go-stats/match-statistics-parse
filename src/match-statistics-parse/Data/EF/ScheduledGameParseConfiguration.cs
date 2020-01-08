using CSGOStats.Services.MatchStatisticsParse.Data.Entities;
using CSGOStats.Services.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSGOStats.Services.MatchStatisticsParse.Data.EF
{
    internal class ScheduledGameParseConfiguration : IEntityTypeConfiguration<ScheduledGameParse>
    {
        public void Configure(EntityTypeBuilder<ScheduledGameParse> builder)
        {
            builder.RegisterTable(Service.Name);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Link).IsRequired().HasMaxLength(200);
            builder.Property(x => x.IsProcessed).IsRequired();
        }
    }
}