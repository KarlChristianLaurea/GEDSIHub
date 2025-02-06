using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddedActivityEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ActualCost",
                table: "Activities",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActualResult",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentTypeBudgetReport",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentTypeEvaluationAttendance",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentTypePictures",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentTypeSpecialOrder",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileDataBudgetReport",
                table: "Activities",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileDataEvaluationAttendance",
                table: "Activities",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileDataPictures",
                table: "Activities",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileDataSpecialOrder",
                table: "Activities",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileNameBudgetReport",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileNameEvaluationAttendance",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileNamePictures",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileNameSpecialOrder",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSizeBudgetReport",
                table: "Activities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSizeEvaluationAttendance",
                table: "Activities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSizePictures",
                table: "Activities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSizeSpecialOrder",
                table: "Activities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NatureOfEvent",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfParticipants",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizingTeamMembers",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDateBudgetReport",
                table: "Activities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDateEvaluationAttendance",
                table: "Activities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDatePictures",
                table: "Activities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDateSpecialOrder",
                table: "Activities",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualCost",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ActualResult",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ContentTypeBudgetReport",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ContentTypeEvaluationAttendance",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ContentTypePictures",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ContentTypeSpecialOrder",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FileDataBudgetReport",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FileDataEvaluationAttendance",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FileDataPictures",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FileDataSpecialOrder",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FileNameBudgetReport",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FileNameEvaluationAttendance",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FileNamePictures",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FileNameSpecialOrder",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FileSizeBudgetReport",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FileSizeEvaluationAttendance",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FileSizePictures",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FileSizeSpecialOrder",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "NatureOfEvent",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "NumberOfParticipants",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "OrganizingTeamMembers",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "UploadDateBudgetReport",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "UploadDateEvaluationAttendance",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "UploadDatePictures",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "UploadDateSpecialOrder",
                table: "Activities");
        }
    }
}
