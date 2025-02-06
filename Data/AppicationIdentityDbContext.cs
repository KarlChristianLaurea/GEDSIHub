using GADApplication.Identity;
using GADApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GADApplication.Data
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<ResponsibleUnit> ResponsibleUnits { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seeding Responsible Units
            builder.Entity<ResponsibleUnit>().HasData(
                new ResponsibleUnit { Id = 1, Name = "OP", Category = "Offices" },
                new ResponsibleUnit { Id = 2, Name = "SPPO", Category = "Offices" },
                new ResponsibleUnit { Id = 3, Name = "OIA", Category = "Offices" },
                new ResponsibleUnit { Id = 4, Name = "IAO", Category = "Offices" },
                new ResponsibleUnit { Id = 5, Name = "CMO", Category = "Offices" },
                new ResponsibleUnit { Id = 6, Name = "ULCO", Category = "Offices" },
                new ResponsibleUnit { Id = 7, Name = "OUBS", Category = "Offices" },
                new ResponsibleUnit { Id = 8, Name = "OVPRED", Category = "Departments" },
                new ResponsibleUnit { Id = 9, Name = "RIHSD", Category = "Departments" },
                new ResponsibleUnit { Id = 10, Name = "RISFI", Category = "Departments" },
                new ResponsibleUnit { Id = 11, Name = "GADO", Category = "Departments" },
                new ResponsibleUnit { Id = 12, Name = "RIST", Category = "Departments" },
                new ResponsibleUnit { Id = 13, Name = "RICL", Category = "Departments" },
                new ResponsibleUnit { Id = 14, Name = "RPO", Category = "Departments" },
                new ResponsibleUnit { Id = 15, Name = "IQMSO", Category = "Departments" },
                new ResponsibleUnit { Id = 16, Name = "IPMO", Category = "Departments" },
                new ResponsibleUnit { Id = 17, Name = "EMO", Category = "Departments" },
                new ResponsibleUnit { Id = 18, Name = "RMO", Category = "Departments" },
                new ResponsibleUnit { Id = 19, Name = "Unisan, Quezon Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 20, Name = "Sto. Tomas, Batangas Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 21, Name = "Sta. Rosa, Laguna Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 22, Name = "San Pedro, Laguna Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 23, Name = "Sablayan, Occidental Mindoro Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 24, Name = "Ragay, Camarines Sur Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 25, Name = "Gen. Luna, Quezon Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 26, Name = "Mulanay, Quezon Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 27, Name = "Alfonso, Cavite Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 28, Name = "Maragondon, Cavite Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 29, Name = "Lopez, Quezon Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 30, Name = "Calauan, Laguna Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 31, Name = "Biñan, Laguna Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 32, Name = "Bansud, Oriental Mindoro Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 33, Name = "Sta. Maria, Bulacan Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 34, Name = "Pulilan, Bulacan Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 35, Name = "Mariveles, Bataan Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 36, Name = "Cabiao, Nueva Ecija Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 37, Name = "Taguig City Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 38, Name = "San Juan City Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 39, Name = "Quezon City Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 40, Name = "Parañaque City Campus", Category = "Campuses" },
                new ResponsibleUnit { Id = 41, Name = "OVPC", Category = "Departments" },
                new ResponsibleUnit { Id = 42, Name = "OVPA", Category = "Departments" },
                new ResponsibleUnit { Id = 43, Name = "CRO", Category = "Departments" },
                new ResponsibleUnit { Id = 44, Name = "PPDO", Category = "Departments" },
                new ResponsibleUnit { Id = 45, Name = "MHDPC", Category = "Departments" },
                new ResponsibleUnit { Id = 46, Name = "GSO", Category = "Departments" },
                new ResponsibleUnit { Id = 47, Name = "DRI", Category = "Departments" },
                new ResponsibleUnit { Id = 48, Name = "PSMO", Category = "Departments" },
                new ResponsibleUnit { Id = 49, Name = "PMO", Category = "Departments" },
                new ResponsibleUnit { Id = 50, Name = "USSO", Category = "Departments" },
                new ResponsibleUnit { Id = 51, Name = "MSD", Category = "Departments" },
                new ResponsibleUnit { Id = 52, Name = "HRM", Category = "Departments" },
                new ResponsibleUnit { Id = 53, Name = "OVPSAS", Category = "Departments" },
                new ResponsibleUnit { Id = 54, Name = "UCCA", Category = "Departments" },
                new ResponsibleUnit { Id = 55, Name = "SDPO", Category = "Departments" },
                new ResponsibleUnit { Id = 56, Name = "ARCDO", Category = "Departments" },
                new ResponsibleUnit { Id = 57, Name = "OCPS", Category = "Departments" },
                new ResponsibleUnit { Id = 58, Name = "OSFA", Category = "Departments" },
                new ResponsibleUnit { Id = 59, Name = "OSS", Category = "Departments" },
                new ResponsibleUnit { Id = 60, Name = "OUR", Category = "Departments" },
                new ResponsibleUnit { Id = 61, Name = "PFQ", Category = "Departments" },
                new ResponsibleUnit { Id = 62, Name = "RGO", Category = "Departments" },
                new ResponsibleUnit { Id = 63, Name = "IPO", Category = "Departments" },
                new ResponsibleUnit { Id = 64, Name = "FMO", Category = "Departments" },
                new ResponsibleUnit { Id = 65, Name = "BSO", Category = "Departments" },
                new ResponsibleUnit { Id = 66, Name = "AD", Category = "Departments" },
                new ResponsibleUnit { Id = 67, Name = "NALLRC", Category = "Departments" },
                new ResponsibleUnit { Id = 68, Name = "NSTP", Category = "Departments" },
                new ResponsibleUnit { Id = 69, Name = "COED", Category = "Departments" },
                new ResponsibleUnit { Id = 70, Name = "ITECH", Category = "Departments" },
                new ResponsibleUnit { Id = 71, Name = "CTHTM", Category = "Departments" },
                new ResponsibleUnit { Id = 72, Name = "CSSD", Category = "Departments" },
                new ResponsibleUnit { Id = 73, Name = "COS", Category = "Departments" },
                new ResponsibleUnit { Id = 74, Name = "CPSPA", Category = "Departments" },
                new ResponsibleUnit { Id = 75, Name = "CHK", Category = "Departments" },
                new ResponsibleUnit { Id = 76, Name = "CE", Category = "Departments" },
                new ResponsibleUnit { Id = 77, Name = "CCIS", Category = "Departments" },
                new ResponsibleUnit { Id = 78, Name = "COC", Category = "Departments" },
                new ResponsibleUnit { Id = 79, Name = "CBA", Category = "Departments" },
                new ResponsibleUnit { Id = 80, Name = "CAL", Category = "Departments" },
                new ResponsibleUnit { Id = 81, Name = "CAF", Category = "Departments" },
                new ResponsibleUnit { Id = 82, Name = "COL", Category = "Departments" },
                new ResponsibleUnit { Id = 83, Name = "GS", Category = "Departments" },
                new ResponsibleUnit { Id = 84, Name = "OUS", Category = "Departments" },
                new ResponsibleUnit { Id = 85, Name = "UPPO", Category = "Departments" },
                new ResponsibleUnit { Id = 86, Name = "UTLDO", Category = "Departments" },
                new ResponsibleUnit { Id = 87, Name = "FEO", Category = "Departments" },
                new ResponsibleUnit { Id = 88, Name = "QAO", Category = "Departments" },
                new ResponsibleUnit { Id = 89, Name = "OEVP", Category = "Departments" },
                new ResponsibleUnit { Id = 90, Name = "ICT", Category = "Departments" },
                new ResponsibleUnit { Id = 91, Name = "IMO", Category = "Departments" },
                new ResponsibleUnit { Id = 92, Name = "IDSA", Category = "Departments" }
            );
        }
    }

}
