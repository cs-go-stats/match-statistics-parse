using CSGOStats.Services.MatchStatisticsParse.Data.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSGOStats.Services.MatchStatisticsParse.Data.EF.Migrations
{
    public partial class ResetGameProcessedFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: nameof(ScheduledGameParse),
                keyColumn: nameof(ScheduledGameParse.IsProcessed),
                keyValue: true,
                column: nameof(ScheduledGameParse.IsProcessed),
                value: false,
                schema: Service.Name);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
