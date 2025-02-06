using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    /// <inheritdoc />
    public partial class MadeFKToNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_GPBs_GPBId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Mandates_Activities_ActivityId",
                table: "Mandates");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "Mandates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GPBId",
                table: "Activities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_GPBs_GPBId",
                table: "Activities",
                column: "GPBId",
                principalTable: "GPBs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mandates_Activities_ActivityId",
                table: "Mandates",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_GPBs_GPBId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Mandates_Activities_ActivityId",
                table: "Mandates");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "Mandates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GPBId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_GPBs_GPBId",
                table: "Activities",
                column: "GPBId",
                principalTable: "GPBs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mandates_Activities_ActivityId",
                table: "Mandates",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
