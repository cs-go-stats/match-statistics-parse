using Microsoft.EntityFrameworkCore.Migrations;

namespace CSGOStats.Services.MatchStatisticsParse.Data.EF.Migrations
{
    public partial class ReplacedIsProcessedPropertyWithDateTimeValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessed",
                schema: "MatchStatisticsParse",
                table: "ScheduledMapParse");

            migrationBuilder.DropColumn(
                name: "IsProcessed",
                schema: "MatchStatisticsParse",
                table: "ScheduledGameParse");

            migrationBuilder.AddColumn<string>(
                name: "CreatedAt",
                schema: "MatchStatisticsParse",
                table: "ScheduledMapParse",
                nullable: false,
                defaultValue: "2020-02-01T19:56:41.3068314Z (ISO)");

            migrationBuilder.AddColumn<string>(
                name: "ProcessedAt",
                schema: "MatchStatisticsParse",
                table: "ScheduledMapParse",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedAt",
                schema: "MatchStatisticsParse",
                table: "ScheduledGameParse",
                nullable: false,
                defaultValue: "2020-02-01T19:56:41.2912604Z (ISO)");

            migrationBuilder.AddColumn<string>(
                name: "ProcessedAt",
                schema: "MatchStatisticsParse",
                table: "ScheduledGameParse",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "MatchStatisticsParse",
                table: "ScheduledMapParse");

            migrationBuilder.DropColumn(
                name: "ProcessedAt",
                schema: "MatchStatisticsParse",
                table: "ScheduledMapParse");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "MatchStatisticsParse",
                table: "ScheduledGameParse");

            migrationBuilder.DropColumn(
                name: "ProcessedAt",
                schema: "MatchStatisticsParse",
                table: "ScheduledGameParse");

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                schema: "MatchStatisticsParse",
                table: "ScheduledMapParse",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                schema: "MatchStatisticsParse",
                table: "ScheduledGameParse",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
