using Microsoft.EntityFrameworkCore.Migrations;

namespace Legalpedia.Migrations
{
    public partial class Socials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeamId",
                table: "SharedNotes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "AbpUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Intagram",
                table: "AbpUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Linedin",
                table: "AbpUsers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "SharedNotes");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Intagram",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Linedin",
                table: "AbpUsers");
        }
    }
}
