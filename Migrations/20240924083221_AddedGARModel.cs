using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddedGARModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GARs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActualResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalBudgetUsed = table.Column<int>(type: "int", nullable: false),
                    GPBId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GARs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GARs_GPBs_GPBId",
                        column: x => x.GPBId,
                        principalTable: "GPBs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GARs_GPBId",
                table: "GARs",
                column: "GPBId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GARs");
        }
    }
}
