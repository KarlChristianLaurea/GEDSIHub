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

namespace GADApplication.Pages.GARPage
{
    public class GARReviewModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public GARReviewModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public GPB GPB { get; set; }
        public Dictionary<int, List<FileEntity>> ActivityFiles { get; set; } = new Dictionary<int, List<FileEntity>>();
        public bool IsPdfPreview { get; set; }
        public int PdfFileId { get; set; }

            public async Task<IActionResult> OnGetAsync(int id)
            {
                if (id == null) return NotFound();

                GPB = await _context.GPBs
                    .Include(g => g.Activities)
                        .ThenInclude(a => a.Mandates)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (GPB == null) return NotFound();

                foreach (var activity in GPB.Activities)
                {
                    var files = await _context.FileEntities
                        .Where(f => f.ActivityId == activity.Id)
                        .OrderByDescending(f => f.UploadDate)
                        .ToListAsync();
                    ActivityFiles[activity.Id] = files;
                }

                return Page();
            }
        public async Task<IActionResult> OnPostAsync()
        {
            var gpbEntity = await _context.GPBs.FindAsync(GPB.Id);
            if (gpbEntity == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(gpbEntity.UserId);
            if (user == null)
            {
                return NotFound();
            }

            // Update the GPB status based on the selected value
            if (GPB.Status == "Approved with Files")
            {
                gpbEntity.Status = "Approved with Files";
                gpbEntity.ApprovalStatus = "Approved with Files";
            }
            else if (GPB.Status == "Rejected with Files")
            {
                gpbEntity.Status = "Rejected with Files";
                gpbEntity.ApprovalStatus = "Rejected with Files";
            }
            else
            {
                // Handle any other statuses here, if needed
                gpbEntity.Status = GPB.Status;
                gpbEntity.ApprovalStatus = GPB.Status;
            }

            gpbEntity.AdminComments = GPB.AdminComments;

            await _context.SaveChangesAsync();

            // Send an in-app notification to the user about the status change
            var notification = new Notification
            {
                SenderId = user.Id,
                ReceiverId = user.Id,
                Message = $"Your GPB has been {gpbEntity.ApprovalStatus}. Please review the comments for further details.",
                CreatedAt = DateTime.UtcNow,
                IsRead = false,
                IsAdminNotification = false
            };

            _context.Notifications.Add(notification);

            // Load the email template
            string emailTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "EmailTemplate.html");
            string emailTemplate = await System.IO.File.ReadAllTextAsync(emailTemplatePath);

            // Consolidate the message content into one string
            string messageContent = $@"
        Your GPB submission (ID: <strong>{gpbEntity.Id}</strong>) has been <strong>{gpbEntity.ApprovalStatus}</strong>.<br><br>
        <strong>Admin Comments:</strong><br>
        {(!string.IsNullOrEmpty(gpbEntity.AdminComments) ? gpbEntity.AdminComments : "No additional comments provided.")}<br><br>
        Please log in to the system for further details.
    ";

            // Replace placeholders in the email template
            string emailContent = emailTemplate
                .Replace("[RecipientName]", user.UserName)
                .Replace("[MessageContent]", messageContent);

            // Send email notification to the user
            string subject = $"Your GPB Submission Status: {gpbEntity.ApprovalStatus}";
            await _emailSender.SendEmailAsync(user.Email, subject, emailContent);

            await _context.SaveChangesAsync();

            return RedirectToPage("./FileList");
        }

        public async Task<IActionResult> OnGetDownloadFileAsync(int fileId)
        {
            var file = await _context.FileEntities.FindAsync(fileId);
            if (file == null) return NotFound("File not found.");

            Response.Headers.Add("Content-Disposition", $"attachment; filename={file.FileName}");
            return File(file.FileData, file.ContentType);
        }

        public async Task<IActionResult> OnGetPreviewFileAsync(int fileId)
        {
            var file = await _context.FileEntities.FindAsync(fileId);
            if (file == null) return NotFound("File not found.");

            if (file.ContentType == "application/pdf")
            {
                IsPdfPreview = true;
                PdfFileId = fileId;
            }

            Response.Headers.Add("Content-Disposition", "inline; filename=" + file.FileName);
            return File(file.FileData, file.ContentType);
        }
    }
}
