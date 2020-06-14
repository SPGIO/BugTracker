using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Data.Migrations
{
    public partial class RefactoringDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bugs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BugStatuses",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "BugStatuses",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BugSeverities",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "BugSeverities",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BugsReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    HowToReproduceBug = table.Column<string>(nullable: false),
                    DateReported = table.Column<DateTime>(nullable: false),
                    DateFixed = table.Column<DateTime>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    SeverityId = table.Column<int>(nullable: false),
                    ReportedById = table.Column<string>(nullable: false),
                    FixedById = table.Column<string>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugsReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BugsReports_AspNetUsers_FixedById",
                        column: x => x.FixedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BugsReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BugsReports_AspNetUsers_ReportedById",
                        column: x => x.ReportedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BugsReports_BugSeverities_SeverityId",
                        column: x => x.SeverityId,
                        principalTable: "BugSeverities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BugsReports_BugStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "BugStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BugsReports_FixedById",
                table: "BugsReports",
                column: "FixedById");

            migrationBuilder.CreateIndex(
                name: "IX_BugsReports_ProjectId",
                table: "BugsReports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BugsReports_ReportedById",
                table: "BugsReports",
                column: "ReportedById");

            migrationBuilder.CreateIndex(
                name: "IX_BugsReports_SeverityId",
                table: "BugsReports",
                column: "SeverityId");

            migrationBuilder.CreateIndex(
                name: "IX_BugsReports_StatusId",
                table: "BugsReports",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BugsReports");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BugStatuses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "BugStatuses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BugSeverities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "BugSeverities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Bugs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateFixed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateReported = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FixedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HowToReproduceBug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    ReportedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SeverityId = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bugs_AspNetUsers_FixedById",
                        column: x => x.FixedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bugs_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bugs_AspNetUsers_ReportedById",
                        column: x => x.ReportedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bugs_BugSeverities_SeverityId",
                        column: x => x.SeverityId,
                        principalTable: "BugSeverities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bugs_BugStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "BugStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_FixedById",
                table: "Bugs",
                column: "FixedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_ProjectId",
                table: "Bugs",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_ReportedById",
                table: "Bugs",
                column: "ReportedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_SeverityId",
                table: "Bugs",
                column: "SeverityId");

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_StatusId",
                table: "Bugs",
                column: "StatusId");
        }
    }
}
