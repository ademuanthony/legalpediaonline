using Microsoft.EntityFrameworkCore.Migrations;

namespace Legalpedia.Migrations
{
    public partial class Anotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Highlights",
                newName: "Note");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Highlights",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Highlights");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Highlights",
                newName: "Text");
        }
    }
}
