using Microsoft.EntityFrameworkCore.Migrations;

namespace Legalpedia.Migrations
{
    public partial class ProfileDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Intagram",
                table: "AbpUsers",
                newName: "Website");

            migrationBuilder.AddColumn<string>(
                name: "AreaOfPractice",
                table: "AbpUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CallToBarYear",
                table: "AbpUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "AbpUsers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaOfPractice",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "CallToBarYear",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "AbpUsers");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "AbpUsers",
                newName: "Intagram");
        }
    }
}
