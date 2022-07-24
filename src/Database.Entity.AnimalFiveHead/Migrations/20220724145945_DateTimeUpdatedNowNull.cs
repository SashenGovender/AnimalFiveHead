using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Entity.AnimalFiveHead.Migrations
{
  public partial class DateTimeUpdatedNowNull : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<DateTime>(
          name: "DateTimeUpdated",
          table: "tb_PlayerSessionInformation",
          type: "datetime2(7)",
          maxLength: 10,
          nullable: true,
          oldClrType: typeof(DateTime),
          oldType: "datetime2(7)",
          oldMaxLength: 10);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<DateTime>(
          name: "DateTimeUpdated",
          table: "tb_PlayerSessionInformation",
          type: "datetime2(7)",
          maxLength: 10,
          nullable: false,
          defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
          oldClrType: typeof(DateTime),
          oldType: "datetime2(7)",
          oldMaxLength: 10,
          oldNullable: true);
    }
  }
}
