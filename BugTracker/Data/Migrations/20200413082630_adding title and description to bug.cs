using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BugTracker.Data.Migrations
{
    public partial class addingtitleanddescriptiontobug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFixed",
                table: "Bugs",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Bugs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Bugs",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Bugs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFixed",
                table: "Bugs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
