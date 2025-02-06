using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddedVerificationFileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "VerificationFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GARActivityId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificationFile_GARActivities_GARActivityId",
                        column: x => x.GARActivityId,
                        principalTable: "GARActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

           
          

            migrationBuilder.CreateIndex(
                name: "IX_VerificationFile_GARActivityId",
                table: "VerificationFile",
                column: "GARActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "VerificationFile");

            
        }
    }
}
