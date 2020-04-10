using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BugPriority",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugPriority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BugStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bugs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HowToReproduceBug = table.Column<string>(nullable: true),
                    DateReported = table.Column<DateTime>(nullable: false),
                    DateFixed = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: true),
                    PriortyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bugs_BugPriority_PriortyId",
                        column: x => x.PriortyId,
                        principalTable: "BugPriority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bugs_BugStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "BugStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_PriortyId",
                table: "Bugs",
                column: "PriortyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_StatusId",
                table: "Bugs",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bugs");

            migrationBuilder.DropTable(
                name: "BugPriority");

            migrationBuilder.DropTable(
                name: "BugStatus");
        }
    }
}
