using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddedFileEntityDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Add the ActivityId column with a default value of 0
            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "FileEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // 2. OPTIONAL: Update existing records in FileEntities to have a valid ActivityId (choose a valid default or map correctly)
            // Here, I'm assuming that there may be activities with Id=1, but you should adjust this to match your data.
            migrationBuilder.Sql("UPDATE FileEntities SET ActivityId = (SELECT TOP 1 Id FROM Activities) WHERE ActivityId = 0");

            // 3. Create the index for the new ActivityId column
            migrationBuilder.CreateIndex(
                name: "IX_FileEntities_ActivityId",
                table: "FileEntities",
                column: "ActivityId");

            // 4. Add the foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_FileEntities_Activities_ActivityId",
                table: "FileEntities",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileEntities_Activities_ActivityId",
                table: "FileEntities");

            migrationBuilder.DropIndex(
                name: "IX_FileEntities_ActivityId",
                table: "FileEntities");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "FileEntities");
        }
    }
}
