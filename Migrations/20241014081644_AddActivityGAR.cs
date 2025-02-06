using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityGAR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "NatureOfEvent",
                table: "GARActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfParticipants",
                table: "GARActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrganizingTeamMembers",
                table: "GARActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NatureOfEvent",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "NumberOfParticipants",
                table: "GARActivities");

            migrationBuilder.DropColumn(
                name: "OrganizingTeamMembers",
                table: "GARActivities");

            

            
        }
    }
}
