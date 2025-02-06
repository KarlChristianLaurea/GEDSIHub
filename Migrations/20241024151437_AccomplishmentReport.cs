using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    /// <inheritdoc />
    public partial class AccomplishmentReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccomplishmentReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GPBId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccomplishmentReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccomplishmentReports_GPBs_GPBId",
                        column: x => x.GPBId,
                        principalTable: "GPBs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityAccomplishments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    ActualResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActualCost = table.Column<float>(type: "real", nullable: false),
                    NatureOfEvent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfParticipants = table.Column<int>(type: "int", nullable: false),
                    OrganizingTeamMembers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccomplishmentFileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AccomplishmentFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPdf = table.Column<bool>(type: "bit", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccomplishmentReportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityAccomplishments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityAccomplishments_AccomplishmentReports_AccomplishmentReportId",
                        column: x => x.AccomplishmentReportId,
                        principalTable: "AccomplishmentReports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActivityAccomplishments_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccomplishmentReports_GPBId",
                table: "AccomplishmentReports",
                column: "GPBId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityAccomplishments_AccomplishmentReportId",
                table: "ActivityAccomplishments",
                column: "AccomplishmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityAccomplishments_ActivityId",
                table: "ActivityAccomplishments",
                column: "ActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityAccomplishments");

            migrationBuilder.DropTable(
                name: "AccomplishmentReports");
        }
    }
}
