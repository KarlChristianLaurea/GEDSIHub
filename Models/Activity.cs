using System.ComponentModel.DataAnnotations.Schema;

namespace GADApplication.Models
{
    public class Activity
    {
        public int Id { get; set; } // Primary key

        public string? ActivityType { get; set; } // ENUM: Client-Focused, Organization-Focused, Attributed Program
        public string? Cause { get; set; }
        public string? Name { get; set; }
        public string? Objective { get; set; }
        public string? PerformanceIndicators { get; set; }
        public double Budget { get; set; }
        public string? SourceBudget { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public int GPBId { get; set; }
        public virtual GPB? GPB { get; set; }

        // Navigation property to Mandate
        public virtual List<Mandate> Mandates { get; set; } = new List<Mandate>(); // An Activity contains multiple mandates

        // Foreign key to Activity
        // Add navigation property for uploaded files
        public List<FileEntity> Files { get; set; }
    }
}
