using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    /// <inheritdoc />
    public partial class adjustedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_GPBs_GPBId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_GARs_Activities_ActivityId",
                table: "GARs");

            migrationBuilder.DropForeignKey(
                name: "FK_VerificationFile_GARActivities_GARActivityId",
                table: "VerificationFile");

            migrationBuilder.DropIndex(
                name: "IX_GARs_ActivityId",
                table: "GARs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VerificationFile",
                table: "VerificationFile");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "GARs");

            migrationBuilder.DropColumn(
                name: "ActualResult",
                table: "GARs");

            migrationBuilder.DropColumn(
                name: "TotalBudgetUsed",
                table: "GARs");

            migrationBuilder.RenameTable(
                name: "VerificationFile",
                newName: "VerificationFiles");

            migrationBuilder.RenameColumn(
                name: "TotalBudgetUsed",
                table: "GARActivities",
                newName: "ActualCost");

            migrationBuilder.RenameIndex(
                name: "IX_VerificationFile_GARActivityId",
                table: "VerificationFiles",
                newName: "IX_VerificationFiles_GARActivityId");

            migrationBuilder.AlterColumn<int>(
                name: "GPBId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VerificationFiles",
                table: "VerificationFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_GPBs_GPBId",
                table: "Activities",
                column: "GPBId",
                principalTable: "GPBs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationFiles_GARActivities_GARActivityId",
                table: "VerificationFiles",
                column: "GARActivityId",
                principalTable: "GARActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_GPBs_GPBId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_VerificationFiles_GARActivities_GARActivityId",
                table: "VerificationFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VerificationFiles",
                table: "VerificationFiles");

            migrationBuilder.RenameTable(
                name: "VerificationFiles",
                newName: "VerificationFile");

            migrationBuilder.RenameColumn(
                name: "ActualCost",
                table: "GARActivities",
                newName: "TotalBudgetUsed");

            migrationBuilder.RenameIndex(
                name: "IX_VerificationFiles_GARActivityId",
                table: "VerificationFile",
                newName: "IX_VerificationFile_GARActivityId");

            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "GARs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ActualResult",
                table: "GARs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalBudgetUsed",
                table: "GARs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "GPBId",
                table: "Activities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VerificationFile",
                table: "VerificationFile",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GARs_ActivityId",
                table: "GARs",
                column: "ActivityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_GPBs_GPBId",
                table: "Activities",
                column: "GPBId",
                principalTable: "GPBs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GARs_Activities_ActivityId",
                table: "GARs",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationFile_GARActivities_GARActivityId",
                table: "VerificationFile",
                column: "GARActivityId",
                principalTable: "GARActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
