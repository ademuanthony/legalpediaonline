using Microsoft.EntityFrameworkCore.Migrations;

namespace Legalpedia.Migrations
{
    public partial class AnnotationTextTarget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextTarget",
                table: "Anotations",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextTarget",
                table: "Anotations");
        }
    }
}
