using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations.ApplicationIdentityDb
{
    /// <inheritdoc />
    public partial class AddedAppUserResponsibleUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponsibleUnit",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsibleUnit",
                table: "AspNetUsers");
        }
    }
}
