using GADApplication.Data;
using GADApplication.Models;
using GADApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using GADApplication.Identity;
using System.IO;

namespace GADApplication.Pages.GADO.SubmissionSettings
{
    public class SetSubmissionDatesModel : PageModel
    {
        [BindProperty]
        public DateTime SubmissionStartDate { get; set; }

        [BindProperty]
        public DateTime SubmissionEndDate { get; set; }

        private readonly ApplicationDbContext _context;
        private readonly NotificationService _notificationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SetSubmissionDatesModel(ApplicationDbContext context, NotificationService notificationService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _notificationService = notificationService;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            // Retrieve current submission dates (if any)
            var submissionSettings = _context.SubmissionSettings.FirstOrDefault();

            if (submissionSettings != null)
            {
                SubmissionStartDate = submissionSettings.StartDate;
                SubmissionEndDate = submissionSettings.EndDate;
            }
            else
            {
                // Initialize dates to current date if no submission settings exist
                SubmissionStartDate = DateTime.Now;
                SubmissionEndDate = DateTime.Now.AddDays(7);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var submissionSettings = _context.SubmissionSettings.FirstOrDefault()
                             ?? new GADApplication.Models.SubmissionSettings();

            // Set submission start and end dates as chosen by the admin
            submissionSettings.StartDate = SubmissionStartDate;
            submissionSettings.EndDate = SubmissionEndDate;

            // Set submission year to next year
            submissionSettings.SubmissionYear = DateTime.Now.Year + 1;

            if (submissionSettings.Id == 0)
                _context.SubmissionSettings.Add(submissionSettings);

            await _context.SaveChangesAsync();

            // Message content to be dynamically injected
            string message = $"The call for GPB submissions is now open from {SubmissionStartDate:MMMM dd, yyyy} " +
                             $"to {SubmissionEndDate:MMMM dd, yyyy}. Please log in to the system to submit your GPB during this timeframe.";

            // Load the email template
            string emailTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "EmailTemplate.html");
            string emailTemplate = await System.IO.File.ReadAllTextAsync(emailTemplatePath);

            // Replace the placeholder in the template with the actual message
            string emailContent = emailTemplate.Replace("[MessageContent]", message);

            // Notify all users about the submission period
            string subject = $"Call for GPB Submissions Opened for {submissionSettings.SubmissionYear}";
            await _notificationService.NotifyAllUsersAboutEventAsync(subject, emailContent, SubmissionStartDate, SubmissionEndDate);

            return RedirectToPage("/GADO/SubmissionSettings/SetSubmissionDates");
        }


        public async Task<IActionResult> OnPostDeleteAsync()
        {
            // Retrieve the existing submission settings
            var submissionSettings = _context.SubmissionSettings.FirstOrDefault();

            if (submissionSettings != null)
            {
                // Remove the submission settings from the database
                _context.SubmissionSettings.Remove(submissionSettings);
                await _context.SaveChangesAsync();

                // Load the email template
                string emailTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "EmailTemplate.html");
                string emailTemplate = await System.IO.File.ReadAllTextAsync(emailTemplatePath);

                // Prepare the message content
                string message = "The call for GPB submissions is no longer allowed at this time. " +
                                 "Please await further announcements for the next submission period.";

                // Replace the placeholder in the email template with the actual message
                string emailContent = emailTemplate.Replace("[MessageContent]", message);

                // Notify all users that the call for GPB submissions is closed
                string subject = "Call for GPB Submissions Closed";
                await _notificationService.NotifyAllUsersAboutEventAsync(subject, emailContent, DateTime.Now, DateTime.Now);
            }

            return RedirectToPage("/GADO/SubmissionSettings/SetSubmissionDates");
        }
    }
}
