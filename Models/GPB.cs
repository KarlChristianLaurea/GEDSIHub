using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GADApplication.Models
{
    public class GPB
    {
        public int Id { get; set; } // Primary key
        public int Year { get; set; }
        public double TotalGAAOrBudget { get; set; } // Total GAA or Budget
        public string? MFOPAPOrPPA { get; set; } // Assuming this is a code or name
        public string? ResponsibleUnit { get; set; }
        public string? Status { get; set; } // New Status field to handle draft and submit statuses

        // Navigation property to Activity
        public virtual List<Activity> Activities { get; set; } = new List<Activity>();// A GPB contains multiple activities

        // Navigation property for GARs (if GPB has multiple GARs)
        public ICollection<GAR> GARs { get; set; }

        
        public string? ApprovalStatus { get; set; } // Approved, Rejected, Needs Revision
        public string? AdminComments { get; set; } // Comments from admin

        public ICollection<AccomplishmentReport> AccomplishmentReports { get; set; }
        public string UserId { get; set; } // No navigation, just storing the UserId
        public string Email { get; set; }
    }
}
