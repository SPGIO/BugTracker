using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Data.Migrations
{
    public partial class bugsinbugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_BugSeverity_SeverityId",
                table: "Bugs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BugSeverity",
                table: "BugSeverity");

            migrationBuilder.RenameTable(
                name: "BugSeverity",
                newName: "BugSeverities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BugSeverities",
                table: "BugSeverities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_BugSeverities_SeverityId",
                table: "Bugs",
                column: "SeverityId",
                principalTable: "BugSeverities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_BugSeverities_SeverityId",
                table: "Bugs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BugSeverities",
                table: "BugSeverities");

            migrationBuilder.RenameTable(
                name: "BugSeverities",
                newName: "BugSeverity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BugSeverity",
                table: "BugSeverity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_BugSeverity_SeverityId",
                table: "Bugs",
                column: "SeverityId",
                principalTable: "BugSeverity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
