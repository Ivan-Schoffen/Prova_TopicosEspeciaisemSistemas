using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roger.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "livros",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Autor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Disponivel = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_livros", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "livros");
        }
    }
}
