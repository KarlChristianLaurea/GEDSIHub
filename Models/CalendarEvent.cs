using System;
using System.ComponentModel.DataAnnotations;

namespace GADApplication.Models
{
    public class CalendarEvent
    {
        [Key]  // Explicitly marking EventId as primary key
        public int EventId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Start { get; set; }  // Start date and time

        [Required]
        public DateTime End { get; set; }  // End date and time

        [Required]
        public string Location { get; set; }

        [Required]
        public int Attendees { get; set; }

        public bool IsConfirmed { get; set; }
    }
}