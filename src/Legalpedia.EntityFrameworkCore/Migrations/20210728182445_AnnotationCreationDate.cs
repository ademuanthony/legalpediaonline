using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Legalpedia.Migrations
{
    public partial class AnnotationCreationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropPrimaryKey(
            //     name: "PK_rules",
            //     table: "rules");

            migrationBuilder.RenameTable(
                name: "rules",
                newName: "Rules");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Anotations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            // migrationBuilder.AddPrimaryKey(
            //     name: "PK_Rules",
            //     table: "Rules",
            //     column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Rules",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Anotations");

            migrationBuilder.RenameTable(
                name: "Rules",
                newName: "rules");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rules",
                table: "rules",
                column: "Id");
        }
    }
}
