using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shortener1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlMap",
                columns: table => new
                {
                    urlMapId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    originalUrl = table.Column<string>(type: "TEXT", nullable: false),
                    shortenedUrl = table.Column<string>(type: "TEXT", nullable: false),
                    usegCounter = table.Column<int>(type: "INTEGER", nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlMap", x => x.urlMapId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlMap");
        }
    }
}
