using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace infotvbackend.Migrations
{
    public partial class CreateIdentityModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LunchItems",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<long>(nullable: false),
                    Food = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LunchItems", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MotdItems",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotdItems", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NoteItems",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteItems", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LunchItems");

            migrationBuilder.DropTable(
                name: "MotdItems");

            migrationBuilder.DropTable(
                name: "NoteItems");
        }
    }
}
