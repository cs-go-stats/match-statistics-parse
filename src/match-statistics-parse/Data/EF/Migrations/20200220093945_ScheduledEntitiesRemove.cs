using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSGOStats.Services.MatchStatisticsParse.Data.EF.Migrations
{
    public partial class ScheduledEntitiesRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledGameParse",
                schema: "MatchStatisticsParse");

            migrationBuilder.DropTable(
                name: "ScheduledMapParse",
                schema: "MatchStatisticsParse");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MatchStatisticsParse");

            migrationBuilder.CreateTable(
                name: "ScheduledGameParse",
                schema: "MatchStatisticsParse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<string>(type: "text", nullable: false, defaultValue: "2020-02-01T19:56:41.2912604Z (ISO)"),
                    Link = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ProcessedAt = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledGameParse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledMapParse",
                schema: "MatchStatisticsParse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<string>(type: "text", nullable: false, defaultValue: "2020-02-01T19:56:41.3068314Z (ISO)"),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    Link = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Map = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ProcessedAt = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledMapParse", x => x.Id);
                });
        }
    }
}
