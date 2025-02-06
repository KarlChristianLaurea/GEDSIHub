using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using GADApplication.Identity;
using GADApplication.Services;
namespace GADApplication.Pages.GPBPage
{
    public class ReviewModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly NotificationService _notificationService;

        public ReviewModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, NotificationService notificationService)
        {
            _context = context;
            _userManager = userManager;
            _notificationService = notificationService;
        }

        [BindProperty]
        public GPBViewModel GPB { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var gpbEntity = await _context.GPBs
                .Include(g => g.Activities)
                    .ThenInclude(a => a.Mandates)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (gpbEntity == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(gpbEntity.UserId);

            if (user == null)
            {
                return NotFound();
            }

            GPB = new GPBViewModel
            {
                Id = gpbEntity.Id,
                Year = gpbEntity.Year,
                TotalGAAOrBudget = gpbEntity.TotalGAAOrBudget,
                MFOPAPOrPPA = gpbEntity.MFOPAPOrPPA,
                ResponsibleUnit = gpbEntity.ResponsibleUnit,
                Status = gpbEntity.Status,
                ApprovalStatus = gpbEntity.ApprovalStatus,
                AdminComments = gpbEntity.AdminComments,
                Activities = gpbEntity.Activities.Select(a => new ActivityViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    ActivityType = a.ActivityType,
                    Cause = a.Cause,
                    Objective = a.Objective,
                    PerformanceIndicators = a.PerformanceIndicators,
                    Budget = a.Budget,
                    SourceBudget = a.SourceBudget,
                    Mandates = a.Mandates.Select(m => new MandateViewModel
                    {
                        Id = m.Id,
                        Description = m.Description
                    }).ToList()
                }).ToList()
            };

            ViewData["UserEmail"] = user.Email;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var gpbEntity = await _context.GPBs
                .FirstOrDefaultAsync(g => g.Id == GPB.Id);

            if (gpbEntity == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(gpbEntity.UserId);

            if (user == null)
            {
                return NotFound();
            }

            // Update ApprovalStatus and Status based on the selected option
            if (GPB.ApprovalStatus == "Approved")
            {
                gpbEntity.ApprovalStatus = "Approved and Ready for File Upload";
                gpbEntity.Status = "Approved and Ready for File Upload";
            }
            else if (GPB.ApprovalStatus == "Needs Revision")
            {
                gpbEntity.ApprovalStatus = "Needs Revision";
                gpbEntity.Status = "Needs Revision";
            }

            // Set the admin comments
            gpbEntity.AdminComments = GPB.AdminComments;
            gpbEntity.Email = user.Email;

            await _context.SaveChangesAsync();

            // Send notification and email to the user
            await _notificationService.SendApprovalEmailAsync(user, gpbEntity.ApprovalStatus, gpbEntity.AdminComments, gpbEntity);

            var notification = new Notification
            {
                SenderId = user.Id, // Ensure this is not null
                ReceiverId = user.Id,
                Message = $"Your GPB has been {gpbEntity.ApprovalStatus}. Please check the comments for more details.",
                CreatedAt = DateTime.UtcNow,
                IsRead = false,
                IsAdminNotification = false
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }



        public class GPBViewModel
        {
            public int Id { get; set; }

            [Required]
            public int Year { get; set; }

            [Required]
            [Range(0, double.MaxValue, ErrorMessage = "Total budget must be a positive number.")]
            public double TotalGAAOrBudget { get; set; }

            [Required]
            public string? MFOPAPOrPPA { get; set; } // Name or code for the MFOPAP/PPA

            [Required]
            public string? ResponsibleUnit { get; set; }

            public string? Status { get; set; } // For draft, submitted, etc.

            // Approval fields
            public string? ApprovalStatus { get; set; } // Approved, Needs Revision, Rejected

            public string? AdminComments { get; set; } // Comments for the revision

            // List of activities associated with this GPB
            public List<ActivityViewModel> Activities { get; set; } = new List<ActivityViewModel>();
        }

        public class ActivityViewModel
        {
            public int Id { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            public string ActivityType { get; set; } // Type of activity

            [Required]
            public string Objective { get; set; } // Objective of the activity

            [Required]
            public string Cause { get; set; } // Cause of the activity

            [Required]
            public string PerformanceIndicators { get; set; } // Performance indicators

            [Required]
            [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive number.")]
            public double Budget { get; set; }

            [Required]
            public string SourceBudget { get; set; } // Source of the budget

            // List of mandates associated with this activity
            public List<MandateViewModel> Mandates { get; set; } = new List<MandateViewModel>();
        }

        public class MandateViewModel
        {
            public int Id { get; set; }

            [Required]
            public string Description { get; set; } // Description of the mandate
        }
    }
}