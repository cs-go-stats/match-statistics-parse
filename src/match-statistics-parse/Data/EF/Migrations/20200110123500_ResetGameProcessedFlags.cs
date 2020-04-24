using Microsoft.EntityFrameworkCore.Migrations;

namespace CSGOStats.Services.MatchStatisticsParse.Data.EF.Migrations
{
    public partial class ResetGameProcessedFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ScheduledGameParse",
                keyColumn: "IsProcessed",
                keyValue: true,
                column: "IsProcessed",
                value: false,
                schema: Service.Name);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
