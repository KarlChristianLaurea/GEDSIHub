using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    /// <inheritdoc />
    public partial class addedNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MandateViewModel");

            migrationBuilder.DropTable(
                name: "ActivityViewModel");

            migrationBuilder.DropTable(
                name: "GPBViewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "Notifications");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "Notification");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GPBViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MFOPAPOrPPA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponsibleUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalGAAOrBudget = table.Column<double>(type: "float", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPBViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<float>(type: "real", nullable: false),
                    Cause = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GPBViewModelId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Objective = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerformanceIndicators = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceBudget = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityViewModel_GPBViewModel_GPBViewModelId",
                        column: x => x.GPBViewModelId,
                        principalTable: "GPBViewModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MandateViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityViewModelId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepublicAct = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandateViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MandateViewModel_ActivityViewModel_ActivityViewModelId",
                        column: x => x.ActivityViewModelId,
                        principalTable: "ActivityViewModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityViewModel_GPBViewModelId",
                table: "ActivityViewModel",
                column: "GPBViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MandateViewModel_ActivityViewModelId",
                table: "MandateViewModel",
                column: "ActivityViewModelId");
        }
    }
}
