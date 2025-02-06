
    namespace GADApplication.Models
    {
        public class GAR
        {
            public int Id { get; set; }
            //public string ActualResult { get; set; }
            //public int TotalBudgetUsed { get; set; }

            // Foreign Key to GPB
            public int? GPBId { get; set; }
            public GPB? GPB { get; set; }

            public List<GARActivity>? GARActivities { get; internal set; }
        public string? Status { get; set; } // New field for GAR-specific status (e.g., "Approved", "Returned")

        // Other fields specific to GAR can be added here as needed
        public DateTime SubmissionDate { get; set; }
        public string? Remarks { get; set; }
        public string? UserId { get; set; }
    }
    }
