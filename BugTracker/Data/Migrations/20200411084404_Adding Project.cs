using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Data.Migrations
{
    public partial class AddingProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_BugPriority_PriortyId",
                table: "Bugs");

            migrationBuilder.DropTable(
                name: "BugPriority");

            migrationBuilder.DropIndex(
                name: "IX_Bugs_PriortyId",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "PriortyId",
                table: "Bugs");

            migrationBuilder.AddColumn<string>(
                name: "FixedById",
                table: "Bugs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportedById",
                table: "Bugs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeverityId",
                table: "Bugs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BugSeverity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugSeverity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_FixedById",
                table: "Bugs",
                column: "FixedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_ReportedById",
                table: "Bugs",
                column: "ReportedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_SeverityId",
                table: "Bugs",
                column: "SeverityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_AspNetUsers_FixedById",
                table: "Bugs",
                column: "FixedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_AspNetUsers_ReportedById",
                table: "Bugs",
                column: "ReportedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_BugSeverity_SeverityId",
                table: "Bugs",
                column: "SeverityId",
                principalTable: "BugSeverity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_AspNetUsers_FixedById",
                table: "Bugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_AspNetUsers_ReportedById",
                table: "Bugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_BugSeverity_SeverityId",
                table: "Bugs");

            migrationBuilder.DropTable(
                name: "BugSeverity");

            migrationBuilder.DropIndex(
                name: "IX_Bugs_FixedById",
                table: "Bugs");

            migrationBuilder.DropIndex(
                name: "IX_Bugs_ReportedById",
                table: "Bugs");

            migrationBuilder.DropIndex(
                name: "IX_Bugs_SeverityId",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "FixedById",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "ReportedById",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "SeverityId",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "PriortyId",
                table: "Bugs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BugPriority",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugPriority", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_PriortyId",
                table: "Bugs",
                column: "PriortyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_BugPriority_PriortyId",
                table: "Bugs",
                column: "PriortyId",
                principalTable: "BugPriority",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
