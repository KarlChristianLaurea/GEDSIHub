using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GADApplication.Models
{
    public class GARActivity
    {
        [Key]
        public int Id { get; set; }

        // Link to the GAR (each GARActivity belongs to a specific GAR)
        public int GARId { get; set; }
        public virtual GAR? GAR { get; set; }

        // Link to the Activity
        public int ActivityId { get; set; }
        public virtual Activity? Activity { get; set; }

        // Actual Budget used for this specific activity
        [Required(ErrorMessage = "Actual Cost is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Actual Cost must be a positive number.")]
        public double ActualCost { get; set; }

        // Actual result of this activity
        [Required(ErrorMessage = "Actual Result is required.")]
        public string ActualResult { get; set; }

        // Nature of the Event
        [Required(ErrorMessage = "Nature of Event is required.")]
        public string NatureOfEvent { get; set; }

        // Number of Participants
        [Required(ErrorMessage = "Number of Participants is required.")]
        public int NumberOfParticipants { get; set; }

        // Name of Organizing Team Members
        [Required(ErrorMessage = "Organizing Team Members are required.")]
        public string OrganizingTeamMembers { get; set; }
        // Special Order File properties
        public string FileNameSpecialOrder { get; set; }
        public string ContentTypeSpecialOrder { get; set; }
        public byte[] FileDataSpecialOrder { get; set; }
        public long FileSizeSpecialOrder { get; set; }
        public DateTime UploadDateSpecialOrder { get; set; }

        // Pictures File properties
        public string FileNamePictures { get; set; }
        public string ContentTypePictures { get; set; }
        public byte[] FileDataPictures { get; set; }
        public long FileSizePictures { get; set; }
        public DateTime UploadDatePictures { get; set; }

        // Budget Report File properties
        public string FileNameBudgetReport { get; set; }
        public string ContentTypeBudgetReport { get; set; }
        public byte[] FileDataBudgetReport { get; set; }
        public long FileSizeBudgetReport { get; set; }
        public DateTime UploadDateBudgetReport { get; set; }

        // Evaluation Attendance File properties
        public string FileNameEvaluationAttendance { get; set; }
        public string ContentTypeEvaluationAttendance { get; set; }
        public byte[] FileDataEvaluationAttendance { get; set; }
        public long FileSizeEvaluationAttendance { get; set; }
        public DateTime UploadDateEvaluationAttendance { get; set; }


    }
}
