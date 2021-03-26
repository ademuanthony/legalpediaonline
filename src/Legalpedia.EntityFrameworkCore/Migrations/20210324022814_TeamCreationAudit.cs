using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Legalpedia.Migrations
{
    public partial class TeamCreationAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Teams");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Teams",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Teams",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Teams");

            migrationBuilder.AddColumn<long>(
                name: "CreatorId",
                table: "Teams",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
