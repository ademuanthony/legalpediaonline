using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Legalpedia.Migrations
{
    public partial class Search : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "indexes",
                columns: table => new
                {
                    rowid = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<string>(type: "text", nullable: true),
                    SuitNo = table.Column<string>(type: "text", nullable: true),
                    title = table.Column<string>(type: "text", nullable: true),
                    header = table.Column<string>(type: "text", nullable: true),
                    court = table.Column<string>(type: "text", nullable: true),
                    judgedate = table.Column<string>(type: "text", nullable: true),
                    subbody = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_indexes", x => x.rowid);
                });

            migrationBuilder.CreateTable(
                name: "keyword_rankings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    keyword_id = table.Column<long>(type: "bigint", nullable: false),
                    rank = table.Column<long>(type: "bigint", nullable: false),
                    index_id = table.Column<long>(type: "bigint", nullable: false),
                    SuitNo = table.Column<string>(type: "text", nullable: true),
                    title = table.Column<string>(type: "text", nullable: true),
                    header = table.Column<string>(type: "text", nullable: true),
                    court = table.Column<string>(type: "text", nullable: true),
                    judgedate = table.Column<string>(type: "text", nullable: true),
                    subbody = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_keyword_rankings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "keywords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    text = table.Column<string>(type: "text", nullable: true),
                    ratio_count = table.Column<int>(type: "integer", nullable: false),
                    summary_count = table.Column<int>(type: "integer", nullable: false),
                    result_count = table.Column<int>(type: "integer", nullable: false),
                    last_indexing_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    version = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_keywords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "indexes");

            migrationBuilder.DropTable(
                name: "keyword_rankings");

            migrationBuilder.DropTable(
                name: "keywords");
        }
    }
}
