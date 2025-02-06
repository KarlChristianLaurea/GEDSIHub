using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GADApplication.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;

namespace GADApplication.Pages.GARPage
{
    public class FileUploadModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public FileUploadModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        [BindProperty]
        public FileEntity FileEntity { get; set; }

        [BindProperty(SupportsGet = true)]
        public int GPBId { get; set; }

        public List<Activity> GPBActivities { get; set; }

        public async Task<IActionResult> OnGetAsync(int gpbId)
        {
            GPBId = gpbId;

            GPBActivities = await _context.Activities
                .Where(a => a.GPBId == GPBId)
                .ToListAsync();

            if (!GPBActivities.Any())
            {
                return NotFound("No activities found for this GPB.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int gpbId)
        {
            GPBId = gpbId;
            GPBActivities = await _context.Activities
                .Where(a => a.GPBId == GPBId)
                .ToListAsync();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var gpb = await _context.GPBs.FindAsync(GPBId);
            if (gpb == null)
            {
                return NotFound("GPB not found.");
            }

            bool filesUploaded = false;

            foreach (var activity in GPBActivities)
            {
                string uploadedFileKey = $"UploadedFiles_{activity.Id}";
                var uploadedFiles = Request.Form.Files.Where(f => f.Name == uploadedFileKey);

                foreach (var uploadedFile in uploadedFiles)
                {
                    if (uploadedFile != null && uploadedFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await uploadedFile.CopyToAsync(memoryStream);

                            var file = new FileEntity
                            {
                                FileName = uploadedFile.FileName,
                                ContentType = uploadedFile.ContentType,
                                FileData = memoryStream.ToArray(),
                                FileSize = uploadedFile.Length,
                                UploadDate = DateTime.Now,
                                ActivityId = activity.Id,
                                ActualCost = double.Parse(Request.Form[$"FileEntityActualCost_{activity.Id}"]),
                                ActualResult = Request.Form[$"FileEntityActualResult_{activity.Id}"],
                                NatureOfEvent = Request.Form[$"FileEntityNatureOfEvent_{activity.Id}"],
                                NumberOfParticipants = int.Parse(Request.Form[$"FileEntityNumberOfParticipants_{activity.Id}"]),
                                OrganizingTeamMembers = Request.Form[$"FileEntityOrganizingTeamMembers_{activity.Id}"],
                                Remarks = Request.Form[$"FileEntityRemarks_{activity.Id}"], // Store the Remarks
                                UserId = user.Id // Store the UserId
                            };

                            _context.FileEntities.Add(file);
                            filesUploaded = true;
                        }
                    }
                }
            }

            if (filesUploaded)
            {
                // Update the GPB status to "Pending with File Upload"
                gpb.Status = gpb.ApprovalStatus = "Pending with File Upload";
                _context.GPBs.Update(gpb);
                await _context.SaveChangesAsync();

                // Notify the admin
                var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                var adminUser = adminUsers.FirstOrDefault();

                if (adminUser != null)
                {
                    // Load the email template
                    string emailTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "EmailTemplate.html");
                    string emailTemplate = await System.IO.File.ReadAllTextAsync(emailTemplatePath);

                    // Create the consolidated message content
                    string messageContent = $@"
                User <strong>{user.UserName}</strong> has uploaded files for approval for GPB ID: <strong>{gpbId}</strong>.<br>
                Please review the submissions at your earliest convenience.<br><br>
                <strong>Details:</strong><br>
                GPB Status: {gpb.Status}<br>
                Uploaded by: {user.FirstName} {user.LastName} ({user.Email})<br>
                Date Submitted: {DateTime.Now:f}<br>
            ";

                    // Replace placeholders in the email template
                    string emailContent = emailTemplate
                        .Replace("[RecipientName]", adminUser.UserName)
                        .Replace("[MessageContent]", messageContent);

                    // Send email notification to the admin
                    string subject = "File Upload Submitted for GPB Approval";
                    await _emailSender.SendEmailAsync(adminUser.Email, subject, emailContent);

                    // Add notification to the database
                    var adminNotification = new Notification
                    {
                        SenderId = user.Id,
                        ReceiverId = adminUser.Id,
                        Role = "Admin",
                        Message = $"User {user.UserName} has uploaded files for approval for GPB ID: {gpbId}.",
                        CreatedAt = DateTime.Now
                    };
                    _context.Notifications.Add(adminNotification);
                }

                // Notify the user
                var userNotification = new Notification
                {
                    SenderId = user.Id,
                    ReceiverId = user.Id,
                    Role = "User",
                    Message = "Your file upload for the GPB has been submitted for approval.",
                    CreatedAt = DateTime.Now
                };
                _context.Notifications.Add(userNotification);

                await _context.SaveChangesAsync();
            }

            // After successful file upload and status update, redirect to the Submitted GPB page
            return RedirectToPage("/GPBPage/SubmittedGPB");
        }

        // Separate method to handle download requests
        public async Task<IActionResult> OnGetDownloadFileAsync(int id)
        {
            var file = await _context.FileEntities.FindAsync(id);

            if (file == null)
            {
                return NotFound();
            }

            // Set the file to download (Content-Disposition: attachment)
            Response.Headers.Add("Content-Disposition", $"attachment; filename={file.FileName}");
            return File(file.FileData, file.ContentType);
        }

        // Method to handle inline preview requests
        public async Task<IActionResult> OnGetFileAsync(int id)
        {
            var file = await _context.FileEntities.FindAsync(id);

            if (file == null)
            {
                return NotFound();
            }

            // Check if the file is a PDF
            bool isPdf = file.IsPdf.HasValue && file.IsPdf.Value;
            ViewData["IsPdf"] = isPdf;

            // Set the file to preview inline (Content-Disposition: inline)
            Response.Headers.Add("Content-Disposition", $"inline; filename={file.FileName}");
            return File(file.FileData, file.ContentType);
        }
    }
}