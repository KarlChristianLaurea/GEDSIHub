using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    /// <inheritdoc />
    public partial class FloatToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AlterColumn<double>(
                name: "Budget",
                table: "Activities",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
       

            migrationBuilder.AlterColumn<float>(
                name: "Budget",
                table: "Activities",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
