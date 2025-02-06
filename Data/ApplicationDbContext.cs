using GADApplication.Models;
using Microsoft.EntityFrameworkCore;
using GADApplication.Pages.GPBPage;
namespace GADApplication.Data
{
    public class ApplicationDbContext: DbContext
    {
        // Constructor accepting DbContextOptions<ApplicationDbContext>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Mandate> Mandates { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<GPB> GPBs { get; set; }
        public DbSet<GAR> GARs { get; set; }
        

        public DbSet<GARActivity> GARActivities { get; set; }

        public DbSet<SubmissionSettings> SubmissionSettings { get; set; }
        public DbSet<FileEntity> FileEntities { get; set; }
        public DbSet<Message> Messages { get; set; }    
        
        public DbSet<AvailableMandates> AvailableMandates { get; set; }
        public DbSet<GARSubmissionSettings> GARSubmissionSettings { get; set; } 
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<AccomplishmentReport> AccomplishmentReports { get; set; }
        
        public DbSet<ActivityAccomplishment> ActivityAccomplishments { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
    }
}
