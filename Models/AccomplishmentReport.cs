namespace GADApplication.Models
{
    public class AccomplishmentReport
    {
        public int Id { get; set; } // Primary Key

        public int GPBId { get; set; } // Foreign Key to GPB

        // Navigation property to the GPB entity
        public GPB GPB { get; set; }

        // Collection of activities associated with the accomplishment report
        public List<ActivityAccomplishment> ActivityAccomplishments { get; set; } = new List<ActivityAccomplishment>();
    }
}
