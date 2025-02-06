using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddedFileEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<string>(
                name: "ResponsibleUnit",
                table: "GPBs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MFOPAPOrPPA",
                table: "GPBs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ContentType1",
                table: "GARActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentType2",
                table: "GARActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentType3",
                table: "GARActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentType4",
                table: "GARActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData1",
                table: "GARActivities",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData2",
                table: "GARActivities",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData3",
                table: "GARActivities",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData4",
                table: "GARActivities",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "FileName1",
                table: "GARActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName2",
                table: "GARActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName3",
                table: "GARActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName4",
                table: "GARActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSize1",
                table: "GARActivities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FileSize2",
                table: "GARActivities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FileSize3",
                table: "GARActivities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FileSize4",
                table: "GARActivities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate1",
                table: "GARActivities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate2",
                table: "GARActivities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate3",
                table: "GARActivities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate4",
                table: "GARActivities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "FileEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileEntities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileEntities");

            

            migrationBuilder.DropColumn(
                name: "ContentType1",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "ContentType2",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "ContentType3",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "ContentType4",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "FileData1",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "FileData2",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "FileData3",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "FileData4",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "FileName1",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "FileName2",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "FileName3",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "FileName4",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "FileSize1",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "FileSize2",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "FileSize3",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "FileSize4",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "UploadDate1",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "UploadDate2",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "UploadDate3",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "UploadDate4",
                table: "GARActivities");

            migrationBuilder.AlterColumn<string>(
                name: "ResponsibleUnit",
                table: "GPBs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MFOPAPOrPPA",
                table: "GPBs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
