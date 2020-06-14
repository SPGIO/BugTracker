using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Data.Migrations
{
    public partial class MakingBugReportFixedByNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BugsReports_AspNetUsers_FixedById",
                table: "BugsReports");

            migrationBuilder.AlterColumn<string>(
                name: "FixedById",
                table: "BugsReports",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_BugsReports_AspNetUsers_FixedById",
                table: "BugsReports",
                column: "FixedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BugsReports_AspNetUsers_FixedById",
                table: "BugsReports");

            migrationBuilder.AlterColumn<string>(
                name: "FixedById",
                table: "BugsReports",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BugsReports_AspNetUsers_FixedById",
                table: "BugsReports",
                column: "FixedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
