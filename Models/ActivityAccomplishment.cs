using System;
namespace GADApplication.Models
{
    public class ActivityAccomplishment
    {
        public int Id { get; set; } // Primary Key

        public int ActivityId { get; set; } // Foreign Key to Activity

        // Navigation property to the corresponding activity entity
        public Activity Activity { get; set; }

        // Fields for actual details to be entered in GAR
        public string ActualResult { get; set; }
        public float ActualCost { get; set; }
        public string NatureOfEvent { get; set; }
        public int NumberOfParticipants { get; set; }
        public string OrganizingTeamMembers { get; set; }
        
        // Binary content of the file
        public byte[] AccomplishmentFileData { get; set; }

        // Optionally, the original name of the file for reference
        public string AccomplishmentFileName { get; set; }

        // The MIME type of the file (optional)
        public string ContentType { get; set; }
        public bool IsPdf { get; set; }  // Boolean indicating if the file is a PDF

        // Optional: Add additional metadata like file size
        public long FileSize { get; set; }

        // Timestamp to track when the file was uploaded
        public DateTime UploadDate { get; set; }
       
    }
}
