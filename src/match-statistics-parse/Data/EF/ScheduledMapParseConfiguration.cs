using CSGOStats.Services.MatchStatisticsParse.Data.Entities;
using CSGOStats.Services.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSGOStats.Services.MatchStatisticsParse.Data.EF
{
    internal class ScheduledMapParseConfiguration : IEntityTypeConfiguration<ScheduledMapParse>
    {
        public void Configure(EntityTypeBuilder<ScheduledMapParse> builder)
        {
            builder.RegisterTable(Service.Name);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.GameId).IsRequired();
            builder.Property(x => x.Link).HasMaxLength(200);
            builder.Property(x => x.Map).IsRequired().HasMaxLength(20);
        }
    }
}