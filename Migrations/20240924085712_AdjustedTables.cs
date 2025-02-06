using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    /// <inheritdoc />
    public partial class AdjustedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "GARs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GARs_ActivityId",
                table: "GARs",
                column: "ActivityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GARs_Activities_ActivityId",
                table: "GARs",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GARs_Activities_ActivityId",
                table: "GARs");

            migrationBuilder.DropIndex(
                name: "IX_GARs_ActivityId",
                table: "GARs");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "GARs");
        }
    }
}
