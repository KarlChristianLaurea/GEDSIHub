using System;
using System.ComponentModel.DataAnnotations;

namespace GADApplication.Models
{
    public class SubmissionSettings
    {
        public int Id { get; set; }  // Primary key

        [Required]
        [StartDateValidation(ErrorMessage = "Start date must be today or later.")]
        public DateTime StartDate { get; set; }  // Start date for submissions

        [Required]
        [EndDateValidation("StartDate", ErrorMessage = "End date must be at least 7 days after the start date.")]
        public DateTime EndDate { get; set; }    // End date for submissions

        public int? SubmissionYear { get; set; }
    }

    // Custom validation attribute to ensure Start Date is today or later
    public class StartDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime startDate)
            {
                if (startDate < DateTime.Now.Date)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }

    // Custom validation attribute to ensure End Date is at least 7 days after Start Date
    public class EndDateValidation : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public EndDateValidation(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var endDate = (DateTime)value;

            var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);
            if (startDateProperty == null)
                return new ValidationResult($"Unknown property: {_startDatePropertyName}");

            var startDateValue = startDateProperty.GetValue(validationContext.ObjectInstance, null);

            if (startDateValue is DateTime startDate)
            {
                // Ensure end date is at least 7 days after the start date
                if (endDate < startDate.AddDays(7))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
