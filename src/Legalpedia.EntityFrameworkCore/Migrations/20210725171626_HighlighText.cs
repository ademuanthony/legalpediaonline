using Microsoft.EntityFrameworkCore.Migrations;

namespace Legalpedia.Migrations
{
    public partial class HighlighText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CaseId",
                table: "Highlights",
                newName: "Text");

            migrationBuilder.AddColumn<string>(
                name: "ContentId",
                table: "Highlights",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContentType",
                table: "Highlights",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SectionId",
                table: "Highlights",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Highlights");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Highlights");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Highlights");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Highlights",
                newName: "CaseId");
        }
    }
}
