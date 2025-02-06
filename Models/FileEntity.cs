namespace GADApplication.Models
{
    public class FileEntity
    {
        // Primary key for the file entity
        public int Id { get; set; }

        // The original name of the uploaded file (e.g., "document.pdf")
        public string FileName { get; set; }

        // The MIME type of the file (e.g., "application/pdf", "image/jpeg")
        public string ContentType { get; set; }

        // The binary content of the file, saved in SQL Server as varbinary(MAX)
        public byte[] FileData { get; set; }

        // Optional: Add additional metadata like file size
        public long FileSize { get; set; }

        // Timestamp to track when the file was uploaded
        public DateTime UploadDate { get; set; }

        // Indicates if the file is a PDF
        public bool? IsPdf { get; set; } = false;

        // Initialize new fields with empty or default values
        public double? ActualCost { get; set; } = 0.0;
        public string? ActualResult { get; set; } = string.Empty;
        public string? NatureOfEvent { get; set; } = string.Empty;
        public int? NumberOfParticipants { get; set; } = 0;
        public string? OrganizingTeamMembers { get; set; } = string.Empty;
        public string? Remarks { get; set; } = string.Empty;


        // Foreign key to Activity
        public int ActivityId { get; set; }
        public Activity Activity { get; set; } // Navigation property

        // Add UserId to track the user who uploaded the file
        public string UserId { get; set; }
    }
}
