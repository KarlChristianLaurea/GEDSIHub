using GADApplication.Data;
using GADApplication.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static GADApplication.Pages.GPBPage.CreateModel;

namespace GADApplication.Pages.GPBPage
{
    public class SubmittedGPBModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SubmittedGPBModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // List to store the submitted GPBs of the current user
        public List<GPBViewModel> GPBs { get; set; }

        // GET request to load the Submitted GPBs page
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                // Retrieve the submission settings
                var submissionSettings = await _context.SubmissionSettings.FirstOrDefaultAsync();
                var currentDate = DateTime.Now;

                // Handle missing submission settings
                if (submissionSettings == null)
                {
                    TempData["ErrorMessage"] = "Submission settings are not configured. Please contact the administrator.";
                    GPBs = new List<GPBViewModel>(); // Ensure the list is initialized
                    return Page();
                }

                // Provide feedback if the submission period is closed
                if (currentDate < submissionSettings.StartDate || currentDate > submissionSettings.EndDate)
                {
                    TempData["InfoMessage"] = "The submission period is currently closed. You can view your submitted GPBs, but no new submissions are allowed.";
                }

                // Fetch the logged-in user
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "You are not authorized to view this page.";
                    return RedirectToPage("/Account/Login");
                }

                // Retrieve the GPBs submitted by the current user
                GPBs = await _context.GPBs
                    .Where(g => g.UserId == user.Id)
                    .Select(g => new GPBViewModel
                    {
                        Id = g.Id,
                        Year = g.Year,
                        TotalGAAOrBudget = g.TotalGAAOrBudget,
                        Status = g.Status,
                        ApprovalStatus = g.ApprovalStatus,
                        AdminComments = g.AdminComments
                    })
                    .ToListAsync();

                return Page(); // Render the page with the fetched GPBs
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
                return RedirectToPage("/Error");
            }
        }

    }
}
