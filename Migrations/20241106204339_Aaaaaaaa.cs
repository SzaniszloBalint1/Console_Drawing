using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Console_Drawing.Migrations
{
    /// <inheritdoc />
    public partial class Aaaaaaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Drawings",
                table: "Drawings");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Drawings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Drawings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drawings",
                table: "Drawings",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Drawings",
                table: "Drawings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Drawings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Drawings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drawings",
                table: "Drawings",
                column: "Name");
        }
    }
}
