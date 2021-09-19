using Microsoft.EntityFrameworkCore.Migrations;

namespace Legalpedia.Migrations
{
    public partial class RemoveHighlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Highlights");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Highlights",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CollectionId = table.Column<string>(type: "text", nullable: true),
                    ContentId = table.Column<string>(type: "text", nullable: true),
                    ContentType = table.Column<int>(type: "integer", nullable: false),
                    Length = table.Column<int>(type: "integer", nullable: false),
                    PageNumber = table.Column<int>(type: "integer", nullable: false),
                    SectionId = table.Column<string>(type: "text", nullable: true),
                    StartIndex = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Highlights", x => x.Id);
                });
        }
    }
}
