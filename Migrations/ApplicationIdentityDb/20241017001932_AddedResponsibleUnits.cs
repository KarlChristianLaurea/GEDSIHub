using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GADApplication.Migrations.ApplicationIdentityDb
{
    /// <inheritdoc />
    public partial class AddedResponsibleUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResponsibleUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsibleUnits", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ResponsibleUnits",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[,]
                {
                    { 1, "Offices", "OP" },
                    { 2, "Offices", "SPPO" },
                    { 3, "Offices", "OIA" },
                    { 4, "Offices", "IAO" },
                    { 5, "Offices", "CMO" },
                    { 6, "Offices", "ULCO" },
                    { 7, "Offices", "OUBS" },
                    { 8, "Departments", "OVPRED" },
                    { 9, "Departments", "RIHSD" },
                    { 10, "Departments", "RISFI" },
                    { 11, "Departments", "GADO" },
                    { 12, "Departments", "RIST" },
                    { 13, "Departments", "RICL" },
                    { 14, "Departments", "RPO" },
                    { 15, "Departments", "IQMSO" },
                    { 16, "Departments", "IPMO" },
                    { 17, "Departments", "EMO" },
                    { 18, "Departments", "RMO" },
                    { 19, "Campuses", "Unisan, Quezon Campus" },
                    { 20, "Campuses", "Sto. Tomas, Batangas Campus" },
                    { 21, "Campuses", "Sta. Rosa, Laguna Campus" },
                    { 22, "Campuses", "San Pedro, Laguna Campus" },
                    { 23, "Campuses", "Sablayan, Occidental Mindoro Campus" },
                    { 24, "Campuses", "Ragay, Camarines Sur Campus" },
                    { 25, "Campuses", "Gen. Luna, Quezon Campus" },
                    { 26, "Campuses", "Mulanay, Quezon Campus" },
                    { 27, "Campuses", "Alfonso, Cavite Campus" },
                    { 28, "Campuses", "Maragondon, Cavite Campus" },
                    { 29, "Campuses", "Lopez, Quezon Campus" },
                    { 30, "Campuses", "Calauan, Laguna Campus" },
                    { 31, "Campuses", "Biñan, Laguna Campus" },
                    { 32, "Campuses", "Bansud, Oriental Mindoro Campus" },
                    { 33, "Campuses", "Sta. Maria, Bulacan Campus" },
                    { 34, "Campuses", "Pulilan, Bulacan Campus" },
                    { 35, "Campuses", "Mariveles, Bataan Campus" },
                    { 36, "Campuses", "Cabiao, Nueva Ecija Campus" },
                    { 37, "Campuses", "Taguig City Campus" },
                    { 38, "Campuses", "San Juan City Campus" },
                    { 39, "Campuses", "Quezon City Campus" },
                    { 40, "Campuses", "Parañaque City Campus" },
                    { 41, "Departments", "OVPC" },
                    { 42, "Departments", "OVPA" },
                    { 43, "Departments", "CRO" },
                    { 44, "Departments", "PPDO" },
                    { 45, "Departments", "MHDPC" },
                    { 46, "Departments", "GSO" },
                    { 47, "Departments", "DRI" },
                    { 48, "Departments", "PSMO" },
                    { 49, "Departments", "PMO" },
                    { 50, "Departments", "USSO" },
                    { 51, "Departments", "MSD" },
                    { 52, "Departments", "HRM" },
                    { 53, "Departments", "OVPSAS" },
                    { 54, "Departments", "UCCA" },
                    { 55, "Departments", "SDPO" },
                    { 56, "Departments", "ARCDO" },
                    { 57, "Departments", "OCPS" },
                    { 58, "Departments", "OSFA" },
                    { 59, "Departments", "OSS" },
                    { 60, "Departments", "OUR" },
                    { 61, "Departments", "PFQ" },
                    { 62, "Departments", "RGO" },
                    { 63, "Departments", "IPO" },
                    { 64, "Departments", "FMO" },
                    { 65, "Departments", "BSO" },
                    { 66, "Departments", "AD" },
                    { 67, "Departments", "NALLRC" },
                    { 68, "Departments", "NSTP" },
                    { 69, "Departments", "COED" },
                    { 70, "Departments", "ITECH" },
                    { 71, "Departments", "CTHTM" },
                    { 72, "Departments", "CSSD" },
                    { 73, "Departments", "COS" },
                    { 74, "Departments", "CPSPA" },
                    { 75, "Departments", "CHK" },
                    { 76, "Departments", "CE" },
                    { 77, "Departments", "CCIS" },
                    { 78, "Departments", "COC" },
                    { 79, "Departments", "CBA" },
                    { 80, "Departments", "CAL" },
                    { 81, "Departments", "CAF" },
                    { 82, "Departments", "COL" },
                    { 83, "Departments", "GS" },
                    { 84, "Departments", "OUS" },
                    { 85, "Departments", "UPPO" },
                    { 86, "Departments", "UTLDO" },
                    { 87, "Departments", "FEO" },
                    { 88, "Departments", "QAO" },
                    { 89, "Departments", "OEVP" },
                    { 90, "Departments", "ICT" },
                    { 91, "Departments", "IMO" },
                    { 92, "Departments", "IDSA" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResponsibleUnits");
        }
    }
}
