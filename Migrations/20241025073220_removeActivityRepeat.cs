using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    /// <inheritdoc />
    public partial class removeActivityRepeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualCost",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ActualResult",
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

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

           
        }
    }
}
