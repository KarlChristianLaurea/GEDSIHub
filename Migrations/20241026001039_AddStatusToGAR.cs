using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToGAR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "GARs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "GARs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmissionDate",
                table: "GARs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "GARs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "GARs");

            migrationBuilder.DropColumn(
                name: "SubmissionDate",
                table: "GARs");
        }
    }
}
