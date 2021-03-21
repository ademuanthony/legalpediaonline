using Microsoft.EntityFrameworkCore.Migrations;

namespace Legalpedia.Migrations
{
    public partial class RMTeamLogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Teams");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "SharedNotes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TargetUserId",
                table: "SharedNotes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NoteRatings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    NoteId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteRatings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteRatings");

            migrationBuilder.DropColumn(
                name: "TargetUserId",
                table: "SharedNotes");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Teams",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SharedNotes",
                type: "text",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
