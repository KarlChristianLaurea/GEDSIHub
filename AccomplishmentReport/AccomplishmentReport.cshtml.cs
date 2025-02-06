using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GADApplication.Pages.AccomplishmentPage
{
    public class AccomplishmentReportModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AccomplishmentReportModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AccomplishmentReportViewModel AccomplishmentReport { get; set; }

        public async Task<IActionResult> OnGetAsync(int gpbId)
        {
            var gpbEntity = await _context.GPBs
                .Include(g => g.Activities)
                .ThenInclude(a => a.Mandates)
                .FirstOrDefaultAsync(g => g.Id == gpbId);

            if (gpbEntity == null)
            {
                return NotFound();
            }

            AccomplishmentReport = new AccomplishmentReportViewModel
            {
                GPBId = gpbEntity.Id,
                Activities = gpbEntity.Activities.Select(activity => new ActivityAccomplishmentViewModel
                {
                    ActivityId = activity.Id,
                    ActivityName = activity.Name,
                    Mandates = activity.Mandates.Select(m => new MandateViewModel
                    {
                        Id = m.Id,
                        Description = m.Description
                    }).ToList()
                }).ToList()
            };

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var accomplishmentReport = new GADApplication.Models.AccomplishmentReport
            {
                GPBId = AccomplishmentReport.GPBId,
                ActivityAccomplishments = new List<ActivityAccomplishment>()
            };

            foreach (var activity in AccomplishmentReport.Activities)
            {
                var activityAccomplishment = new ActivityAccomplishment
                {
                    ActivityId = activity.ActivityId,
                    ActualResult = activity.ActualResult,
                    ActualCost = activity.ActualCost,
                    NatureOfEvent = activity.NatureOfEvent,
                    NumberOfParticipants = activity.NumberOfParticipants,
                    OrganizingTeamMembers = activity.OrganizingTeamMembers
                };

                // Check if a file has been uploaded for this activity
                if (activity.AccomplishmentFile != null && activity.AccomplishmentFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        // Copy the uploaded file to the memory stream
                        await activity.AccomplishmentFile.CopyToAsync(memoryStream);

                        // Save file data to the activity accomplishment model
                        activityAccomplishment.AccomplishmentFileData = memoryStream.ToArray(); // Save binary data
                        activityAccomplishment.AccomplishmentFileName = activity.AccomplishmentFile.FileName; // Save original file name
                        activityAccomplishment.ContentType = activity.AccomplishmentFile.ContentType; // Set the MIME type (e.g., application/pdf)
                        activityAccomplishment.FileSize = activity.AccomplishmentFile.Length; // Save file size
                        activityAccomplishment.UploadDate = DateTime.Now; // Record the upload date

                        // Check if the file is a PDF
                        activityAccomplishment.IsPdf = activity.AccomplishmentFile.ContentType == "application/pdf";
                    }
                }

                // Add the activity accomplishment to the report
                accomplishmentReport.ActivityAccomplishments.Add(activityAccomplishment);
            }

            // Add the accomplishment report to the database
            _context.AccomplishmentReports.Add(accomplishmentReport);
            await _context.SaveChangesAsync();

            // Redirect back to the index or some confirmation page
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnGetDownloadFileAsync(int id)
        {
            var activityAccomplishment = await _context.ActivityAccomplishments.FindAsync(id);

            if (activityAccomplishment == null || activityAccomplishment.AccomplishmentFileData == null)
            {
                return NotFound();
            }

            // Set the file for download
            Response.Headers.Add("Content-Disposition", $"attachment; filename={activityAccomplishment.AccomplishmentFileName}");
            return File(activityAccomplishment.AccomplishmentFileData, activityAccomplishment.ContentType);
        }

        // Method to handle inline preview requests
        // Preview the file (only for PDFs)
        public async Task<IActionResult> OnGetFileAsync(int id)
        {
            var activityAccomplishment = await _context.ActivityAccomplishments.FindAsync(id);

            if (activityAccomplishment == null || activityAccomplishment.AccomplishmentFileData == null)
            {
                return NotFound();
            }

            // Check if the file is a PDF
            bool isPdf = activityAccomplishment.ContentType == "application/pdf";

            if (isPdf)
            {
                // Inline preview for PDF
                Response.Headers.Add("Content-Disposition", $"inline; filename={activityAccomplishment.AccomplishmentFileName}");
                return File(activityAccomplishment.AccomplishmentFileData, activityAccomplishment.ContentType);
            }
            else
            {
                // If not a PDF, allow download instead
                return RedirectToPage("DownloadFile", new { id });
            }

        }

        public class AccomplishmentReportViewModel
        {
            public int GPBId { get; set; }
            public List<ActivityAccomplishmentViewModel> Activities { get; set; } = new List<ActivityAccomplishmentViewModel>();
        }

        public class ActivityAccomplishmentViewModel
        {
            public int ActivityId { get; set; }
            public string ActivityName { get; set; }

            // Additional fields for Accomplishment Report
            [Required(ErrorMessage = "Actual result is required")]
            public string ActualResult { get; set; }

            [Required(ErrorMessage = "Actual cost is required")]
            public float ActualCost { get; set; }

            [Required(ErrorMessage = "Nature of Event is required.")]
            public string NatureOfEvent { get; set; }

            [Required(ErrorMessage = "Number of Participants is required.")]
            public int NumberOfParticipants { get; set; }

            [Required(ErrorMessage = "Organizing Team Members are required.")]
            public string OrganizingTeamMembers { get; set; }

            public List<MandateViewModel> Mandates { get; set; } = new List<MandateViewModel>();
            // New property to hold uploaded file names

            // File upload property
            public IFormFile AccomplishmentFile { get; set; }
            // Add these properties to hold the uploaded file data in the ViewModel
            public string AccomplishmentFileName { get; set; }
            public string ContentType { get; set; }  // Add this property for the MIME type (e.g., application/pdf)
            public bool IsPdf { get; set; }  // Boolean indicating if the file is a PDF

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