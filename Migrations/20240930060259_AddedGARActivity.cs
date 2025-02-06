using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    public partial class AddedGARActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GARActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GARId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    TotalBudgetUsed = table.Column<double>(type: "float", nullable: false),
                    ActualResult = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GARActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GARActivities_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GARActivities_GARs_GARId",
                        column: x => x.GARId,
                        principalTable: "GARs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // Set to Restrict to avoid multiple cascade paths
                });

            migrationBuilder.CreateIndex(
                name: "IX_GARActivities_ActivityId",
                table: "GARActivities",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_GARActivities_GARId",
                table: "GARActivities",
                column: "GARId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GARActivities");
        }
    }
}
