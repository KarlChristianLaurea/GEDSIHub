using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using GADApplication.Identity;
using GADApplication.Services;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GADApplication.Pages.Calendar
{
    public class AddEventModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly NotificationService _notificationService;

        public AddEventModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, NotificationService notificationService)
        {
            _dbContext = context;
            _userManager = userManager;
            _notificationService = notificationService;
        }

        [BindProperty]
        public CalendarEvent EventData { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validate the form before proceeding
            // if (!ModelState.IsValid)
            // {
            //     return Page();
            // }

            // Create the new calendar event
            var newEvent = new CalendarEvent
            {
                Name = EventData.Name,
                Description = EventData.Description,
                Location = EventData.Location,
                Attendees = EventData.Attendees,
                IsConfirmed = EventData.IsConfirmed,
                Start = EventData.Start,
                End = EventData.End
            };

            // Add the event to the database
            _dbContext.CalendarEvents.Add(newEvent);
            await _dbContext.SaveChangesAsync();

            // Load the email template
            string emailTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "EmailTemplate.html");
            string emailTemplate = await System.IO.File.ReadAllTextAsync(emailTemplatePath);

            // Construct the message content with all event details
            string messageContent = $@"
        A new event has been added to the calendar!<br><br>
        <strong>Event Name:</strong> {newEvent.Name}<br>
        <strong>Description:</strong> {newEvent.Description}<br>
        <strong>Location:</strong> {newEvent.Location}<br>
        <strong>Start:</strong> {newEvent.Start:f}<br>
        <strong>End:</strong> {newEvent.End:f}<br><br>
        Please log in to view more details and register for the event!
    ";

            // Replace placeholders in the email template
            string emailContent = emailTemplate
                .Replace("[RecipientName]", "All Users") // Replace with individual names if required
                .Replace("[MessageContent]", messageContent);

            // Prepare the subject
            string subject = "New Event Added to Calendar";

            // Use the NotificationService to notify all users
            await _notificationService.NotifyAllUsersAboutEventAsync(subject, emailContent, newEvent.Start, newEvent.End);

            // Redirect back to the Calendar page after successful creation
            return RedirectToPage("/Calendar/Calendar");
        }



    }
}
