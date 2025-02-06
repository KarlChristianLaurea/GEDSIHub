using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using GADApplication.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace GADApplication.Pages.GPBPage
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        // Injecting the database context, UserManager, and IEmailSender via constructor
        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public GPBViewModel GPB { get; set; }
        public List<string> RAList { get; set; } = new List<string>
        {
            "R.A. 10354", "R.A. 10364", "R.A. 10398",
            "R.A. 11036", "R.A. 11166", "R.A. 11313",
            "R.A. 11394", "R.A. 6725", "R.A. 6949",
            "R.A. 7877", "R.A. 8504", "R.A. 9262",
            "R.A. 9710"
        };

        public async Task<IActionResult> OnGetAsync()
        {
            // Fetch the submission settings from the SubmissionSettings entity
            var submissionSettings = await _context.SubmissionSettings.FirstOrDefaultAsync();
            var currentDate = DateTime.Now;

            if (submissionSettings == null)
            {
                TempData["ErrorMessage"] = "Submission settings are not configured. Please contact the administrator.";
                return RedirectToPage("/GPBPage/SubmittedGPB");
            }

            // Check if the current date is within the allowed submission period
            if (currentDate < submissionSettings.StartDate || currentDate > submissionSettings.EndDate)
            {
                TempData["ErrorMessage"] = "GPB submissions are only allowed between " +
                    submissionSettings.StartDate.ToString("MMMM dd, yyyy") + " and " +
                    submissionSettings.EndDate.ToString("MMMM dd, yyyy");
                return RedirectToPage("/GPBPage/SubmittedGPB");
            }

            // Retrieve the submission year and increment it by 1 to reflect the next submission year
            var submissionYear = (submissionSettings.SubmissionYear ?? DateTime.Now.Year) + 1;

            // Fetch the currently logged-in user's information
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            // Check if the user has already submitted for this year
            var existingSubmission = await _context.GPBs
                .AnyAsync(g => g.UserId == user.Id && g.Year == submissionYear);

            if (existingSubmission)
            {
                TempData["ErrorMessage"] = $"You have already submitted a GPB for the year {submissionYear}.";
                return RedirectToPage("/GPBPage/SubmittedGPB");
            }
            

            // Initialize the GPB form with the default year, responsible unit, and default activity
            GPB = new GPBViewModel
            {
                Year = submissionYear ,
                ResponsibleUnit = user.ResponsibleUnit,
                Activities = new List<ActivityViewModel>
        {
            new ActivityViewModel
            {
                Mandates = new List<MandateViewModel>
                {
                    new MandateViewModel()
                }
            }
        }
            };

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(string action)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault();

            // Retrieve submission settings
            var submissionSettings = await _context.SubmissionSettings.FirstOrDefaultAsync();
            var currentDate = DateTime.Now;

            if (submissionSettings == null || currentDate < submissionSettings.StartDate || currentDate > submissionSettings.EndDate)
            {
                TempData["ErrorMessage"] = "GPB submissions are only allowed between " +
                    submissionSettings.StartDate.ToString("MMMM dd, yyyy") + " and " +
                    submissionSettings.EndDate.ToString("MMMM dd, yyyy");
                return RedirectToPage("/Error");
            }

            // Get the submission year from settings
            var submissionYear = submissionSettings.SubmissionYear != 0
                ? submissionSettings.SubmissionYear
                : DateTime.Now.Year + 1;

            // Check if a submission exists for this user for the current submission year
            var existingSubmission = await _context.GPBs
                .AnyAsync(g => g.UserId == user.Id && g.Year == submissionYear);

            if (existingSubmission)
            {
                TempData["ErrorMessage"] = $"You have already submitted a GPB for the year {submissionYear}.";
                return RedirectToPage("/Error");
            }

            // Check if model state is valid
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Map the GPBViewModel data to the GPB entity
            var gpbEntity = new GPB
            {
                Year = GPB.Year,
                TotalGAAOrBudget = GPB.TotalGAAOrBudget,
                MFOPAPOrPPA = GPB.MFOPAPOrPPA,
                ResponsibleUnit = user.ResponsibleUnit,
                Status = action == "Submit" ? "Pending" : "Draft",
                ApprovalStatus = action == "Submit" ? "Pending" : null,
                UserId = user.Id,
                Email = user.Email,
                Activities = new List<Activity>()
            };

            foreach (var activity in GPB.Activities)
            {
                var activityEntity = new Activity
                {
                    ActivityType = activity.ActivityType,
                    Cause = activity.Cause,
                    Name = activity.Name,
                    Objective = activity.Objective,
                    PerformanceIndicators = activity.PerformanceIndicators,
                    Budget = activity.Budget,
                    SourceBudget = activity.SourceBudget,
                    Mandates = new List<Mandate>()
                };

                foreach (var mandate in activity.Mandates)
                {
                    var mandateEntity = new Mandate
                    {
                        Description = mandate.Description,
                        RepublicAct = mandate.RepublicAct
                    };
                    activityEntity.Mandates.Add(mandateEntity);
                }

                gpbEntity.Activities.Add(activityEntity);
            }

            _context.GPBs.Add(gpbEntity);
            await _context.SaveChangesAsync();

            // Fetch the admin user
            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            var adminUser = adminUsers.FirstOrDefault();

            if (adminUser != null)
            {
                // Send a notification to the admin when the form is submitted
                if (action == "Submit")
                {
                    var notification = new Notification
                    {
                        SenderId = user.Id,
                        ReceiverId = adminUser.Id,
                        Role = userRole,
                        Message = $"User {user.UserName} (Role: {userRole}) has submitted a GPB form for approval."
                    };

                    _context.Notifications.Add(notification);
                    await _context.SaveChangesAsync();

                    // Construct the message content
                    string messageContent = $@"
                A new GPB form has been submitted for your approval.<br><br>
                <strong>Submitted by:</strong> {user.UserName} (Role: {userRole})<br>
                <strong>Submission Year:</strong> {submissionYear}<br><br>
                Please log in to the system to review it at your earliest convenience.
            ";

                    // Load the email template
                    string emailTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "EmailTemplate.html");
                    string emailTemplate = await System.IO.File.ReadAllTextAsync(emailTemplatePath);

                    // Replace placeholders in the email template
                    string emailContent = emailTemplate
                        .Replace("[RecipientName]", adminUser.UserName)
                        .Replace("[MessageContent]", messageContent);

                    // Send email notification to the admin
                    string subject = "GPB Form Submitted for Approval";
                    await _emailSender.SendEmailAsync(adminUser.Email, subject, emailContent);

                    TempData["SuccessMessage"] = "GPB has been successfully submitted.";
                }
                else
                {
                    TempData["SuccessMessage"] = "GPB has been saved as a draft.";
                }
            }

            return RedirectToPage("SubmittedGPB");
        }





        // ViewModel classes for handling data from the form
        public class GPBViewModel
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Year is required")]
            public int Year { get; set; }

            [Required(ErrorMessage = "Total GAA or Budget is required")]
            public double TotalGAAOrBudget { get; set; }

            [Required(ErrorMessage = "MFO/PAP or PPA is required")]
            public string? MFOPAPOrPPA { get; set; }

            public string? ResponsibleUnit { get; set; }

            [Required(ErrorMessage = "Status is required")]
            public List<ActivityViewModel> Activities { get; set; } = new List<ActivityViewModel>();
            public string? Status { get; set; }
            public string? ApprovalStatus { get; set; } // Approved, Rejected, Needs Revision
            public string? AdminComments { get; set; } // Comments from admin
        }

        public class ActivityViewModel
        {
            public int Id { get; set; }
            [Required(ErrorMessage = "Activity type is required")]
            public string ActivityType { get; set; }
            [Required(ErrorMessage = "Activity cause is required")]
            public string Cause { get; set; }

            [Required(ErrorMessage = "Activity name is required")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Performance Indicators are required")]
            public string PerformanceIndicators { get; set; }

            [Required(ErrorMessage = "Objective is required")]
            public string Objective { get; set; }

            [Required(ErrorMessage = "Budget is required")]
            public double Budget { get; set; }

            [Required(ErrorMessage = "Budget Source is required")]
            public string SourceBudget { get; set; }

            public List<MandateViewModel> Mandates { get; set; } = new List<MandateViewModel>();
        }

        public class MandateViewModel
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Mandate description is required")]
            public string Description { get; set; }
            [Required(ErrorMessage = "Republic Act is required")]
            public string RepublicAct { get; set; }  // Add field for Republic Act
        }
    }
}