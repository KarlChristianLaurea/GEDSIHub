using System;
using System.ComponentModel.DataAnnotations;

namespace GADApplication.Models
{
    public class GARSubmissionSettings
    {
        public int Id { get; set; }  // Primary key

        [Required]
        [StartDateValidation(ErrorMessage = "Start date must be today or later.")]
        public DateTime StartDate { get; set; }  // Start date for GAR submissions

        [Required]
        [EndDateValidation("StartDate", ErrorMessage = "End date must be at least 7 days after the start date.")]
        public DateTime EndDate { get; set; }    // End date for GAR submissions
    }
}
