using Microsoft.EntityFrameworkCore.Migrations;

namespace Legalpedia.Migrations
{
    public partial class PageNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PageId",
                table: "Bookmarks",
                newName: "PageNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PageNumber",
                table: "Bookmarks",
                newName: "PageId");
        }
    }
}
