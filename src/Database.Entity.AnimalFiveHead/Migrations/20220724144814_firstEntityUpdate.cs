using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Entity.AnimalFiveHead.Migrations
{
  public partial class firstEntityUpdate : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "tb_GameSessionState",
          columns: table => new
          {
            GameStateId = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            GameStateName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_tb_GameSessionState", x => x.GameStateId);
          });

      migrationBuilder.CreateTable(
          name: "tb_PlayerSessionInformation",
          columns: table => new
          {
            SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            PlayerId = table.Column<int>(type: "int", nullable: false),
            Score = table.Column<int>(type: "int", nullable: false),
            Cards = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
            CardIds = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
            GameSession = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false),
            GameResult = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            DateTimeAdded = table.Column<DateTime>(type: "datetime2(7)", maxLength: 10, nullable: false),
            DateTimeUpdated = table.Column<DateTime>(type: "datetime2(7)", maxLength: 10, nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_PlayerSessionInformation", x => new { x.SessionId, x.PlayerId });
          });

      migrationBuilder.CreateTable(
          name: "tb_PlayerType",
          columns: table => new
          {
            PlayerId = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            PlayerName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_tb_PlayerType", x => x.PlayerId);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "tb_GameSessionState");

      migrationBuilder.DropTable(
          name: "tb_PlayerSessionInformation");

      migrationBuilder.DropTable(
          name: "tb_PlayerType");
    }
  }
}
