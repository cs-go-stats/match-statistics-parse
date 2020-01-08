using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSGOStats.Services.MatchStatisticsParse.Data.EF.Migrations
{
    public partial class AddScheduledParseEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MatchStatisticsParse");

            migrationBuilder.CreateTable(
                name: "ScheduledGameParse",
                schema: "MatchStatisticsParse",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Link = table.Column<string>(maxLength: 100, nullable: false),
                    IsProcessed = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    Link = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledMapParse", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledGameParse",
                schema: "MatchStatisticsParse");

            migrationBuilder.DropTable(
                name: "ScheduledMapParse",
                schema: "MatchStatisticsParse");
        }
    }
}
