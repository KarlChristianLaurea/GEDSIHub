using GADApplication.Data;
using GADApplication.Identity;
using GADApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static GADApplication.Pages.GPBPage.CreateModel;

namespace GADApplication.Pages.GPBPage
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        // Inject ApplicationDbContext, UserManager, and IEmailSender
        public EditModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public GADApplication.Pages.GPBPage.CreateModel.GPBViewModel GPB { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Fetch the GPB, Activities, and Mandates for the given GPB ID
            var gpbEntity = await _context.GPBs
                .Include(g => g.Activities)
                    .ThenInclude(a => a.Mandates)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (gpbEntity == null)
            {
                return NotFound();
            }

            // Populate the ViewModel for the Razor Page
            GPB = new GADApplication.Pages.GPBPage.CreateModel.GPBViewModel
            {
                Id = gpbEntity.Id,
                Year = gpbEntity.Year,
                TotalGAAOrBudget = gpbEntity.TotalGAAOrBudget,
                MFOPAPOrPPA = gpbEntity.MFOPAPOrPPA,
                ResponsibleUnit = gpbEntity.ResponsibleUnit,
                Activities = gpbEntity.Activities.Select(a => new ActivityViewModel
                {
                    Id = a.Id,
                    ActivityType = a.ActivityType,
                    Cause = a.Cause,
                    Name = a.Name,
                    Objective = a.Objective,
                    PerformanceIndicators = a.PerformanceIndicators,
                    Budget = a.Budget,
                    SourceBudget = a.SourceBudget,
                    Mandates = a.Mandates.Select(m => new MandateViewModel
                    {
                        Id = m.Id,
                        RepublicAct = m.RepublicAct,
                        Description = m.Description
                    }).ToList()
                }).ToList()
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string action)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Fetch the existing GPB from the database
            var gpbEntity = await _context.GPBs
                .Include(g => g.Activities)
                    .ThenInclude(a => a.Mandates)
                .FirstOrDefaultAsync(g => g.Id == GPB.Id);

            if (gpbEntity == null)
            {
                return NotFound();
            }

            // Update GPB properties
            gpbEntity.Year = GPB.Year;
            gpbEntity.TotalGAAOrBudget = GPB.TotalGAAOrBudget;
            gpbEntity.MFOPAPOrPPA = GPB.MFOPAPOrPPA;
            gpbEntity.ResponsibleUnit = GPB.ResponsibleUnit;

            // Handle updating, adding, or deleting activities and mandates
            UpdateActivities(gpbEntity);

            // If action is "Update and Submit", set the status to "Pending"
            if (action == "Update and Submit")
            {
                gpbEntity.Status = "Pending"; // Set status to "Pending" for approval

                // Send notification to admin if necessary
                var user = await _userManager.GetUserAsync(User);
                var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                var adminUser = adminUsers.FirstOrDefault();

                if (adminUser != null)
                {
                    // Add notification to the database
                    var notification = new Notification
                    {
                        SenderId = user.Id,
                        ReceiverId = adminUser.Id,
                        Role = "Admin",
                        Message = $"User {user.UserName} has updated and submitted a GPB form for your review."
                    };
                    _context.Notifications.Add(notification);

                    // Construct the message content
                    string messageContent = $@"
                A GPB form has been updated and submitted for your review.<br><br>
                <strong>Submitted by:</strong> {user.UserName}<br>
                <strong>Responsible Unit:</strong> {gpbEntity.ResponsibleUnit}<br>
                <strong>Year:</strong> {gpbEntity.Year}<br>
                <strong>Total Budget:</strong> {gpbEntity.TotalGAAOrBudget}<br><br>
                Please log in to the system to review the submission.
            ";

                    // Load the email template
                    string emailTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "EmailTemplate.html");
                    string emailTemplate = await System.IO.File.ReadAllTextAsync(emailTemplatePath);

                    // Replace placeholders in the email template
                    string emailContent = emailTemplate
                        .Replace("[RecipientName]", adminUser.UserName)
                        .Replace("[MessageContent]", messageContent);

                    // Send email notification to the admin
                    string subject = "GPB Form Updated and Submitted for Approval";
                    await _emailSender.SendEmailAsync(adminUser.Email, subject, emailContent);
                }

                TempData["SuccessMessage"] = "GPB has been successfully updated and submitted for review.";
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return RedirectToPage("SubmittedGPB");
        }



        private void UpdateActivities(GPB gpbEntity)
        {
            foreach (var activityVm in GPB.Activities)
            {
                var activityEntity = gpbEntity.Activities.FirstOrDefault(a => a.Id == activityVm.Id);

                if (activityEntity == null)
                {
                    // New Activity
                    activityEntity = new Activity
                    {
                        ActivityType = activityVm.ActivityType,
                        Cause = activityVm.Cause,
                        Name = activityVm.Name,
                        Objective = activityVm.Objective,
                        PerformanceIndicators = activityVm.PerformanceIndicators,
                        Budget = activityVm.Budget,
                        SourceBudget = activityVm.SourceBudget,
                        Mandates = activityVm.Mandates.Select(m => new Mandate
                        {
                            RepublicAct = m.RepublicAct, // Populate Republic Act
                            Description = m.Description
                        }).ToList()
                    };
                    gpbEntity.Activities.Add(activityEntity);
                }
                else
                {
                    // Update existing Activity
                    activityEntity.ActivityType = activityVm.ActivityType;
                    activityEntity.Cause = activityVm.Cause;
                    activityEntity.Name = activityVm.Name;
                    activityEntity.Objective = activityVm.Objective;
                    activityEntity.PerformanceIndicators = activityVm.PerformanceIndicators;
                    activityEntity.Budget = activityVm.Budget;
                    activityEntity.SourceBudget = activityVm.SourceBudget;

                    // Handle mandates
                    UpdateMandates(activityVm, activityEntity);
                }
            }

            // Optionally remove any activities from the database that were removed in the ViewModel
            var activityIds = GPB.Activities.Select(a => a.Id).ToHashSet();
            gpbEntity.Activities.RemoveAll(a => !activityIds.Contains(a.Id));
        }

        private void UpdateMandates(ActivityViewModel activityVm, Activity activityEntity)
        {
            foreach (var mandateVm in activityVm.Mandates)
            {
                var mandateEntity = activityEntity.Mandates.FirstOrDefault(m => m.Id == mandateVm.Id);

                if (mandateEntity == null)
                {
                    mandateEntity = new Mandate
                    {
                        RepublicAct = mandateVm.RepublicAct, // Set Republic Act for new mandates
                        Description = mandateVm.Description
                    };
                    activityEntity.Mandates.Add(mandateEntity);
                }
                else
                {
                    mandateEntity.RepublicAct = mandateVm.RepublicAct; // Update Republic Act
                    mandateEntity.Description = mandateVm.Description;
                }
            }

            var mandateIds = activityVm.Mandates.Select(m => m.Id).ToHashSet();
            activityEntity.Mandates.RemoveAll(m => !mandateIds.Contains(m.Id));
        }
    }
}