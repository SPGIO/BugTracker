using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Data.Migrations
{
    public partial class addingto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_BugStatus_StatusId",
                table: "Bugs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BugStatus",
                table: "BugStatus");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "BugStatus",
                newName: "BugStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BugStatuses",
                table: "BugStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_BugStatuses_StatusId",
                table: "Bugs",
                column: "StatusId",
                principalTable: "BugStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_BugStatuses_StatusId",
                table: "Bugs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BugStatuses",
                table: "BugStatuses");

            migrationBuilder.RenameTable(
                name: "BugStatuses",
                newName: "BugStatus");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BugStatus",
                table: "BugStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_BugStatus_StatusId",
                table: "Bugs",
                column: "StatusId",
                principalTable: "BugStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
