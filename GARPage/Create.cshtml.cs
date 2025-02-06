using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GADApplication.Pages.GARPage.CreateGARModels
{
    public class CreateGARModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateGARModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CreateGARViewModel GAR { get; set; }
        [BindProperty]
        public GPBViewModel GPB { get; set; }

        // This method fetches the GPB and its activities based on the provided GPBId
        public async Task<IActionResult> OnGetAsync(int gpbId)
        {            // Fetch the GPB and its associated Activities
                     // Fetch submission settings for GAR
            var garSubmissionSettings = await _context.GARSubmissionSettings.FirstOrDefaultAsync();
            if (garSubmissionSettings != null)
            {
                if (DateTime.Now < garSubmissionSettings.StartDate || DateTime.Now > garSubmissionSettings.EndDate)
                {
                    ModelState.AddModelError(string.Empty, "GAR submissions are only allowed between " +
                        garSubmissionSettings.StartDate.ToString("MMMM dd, yyyy hh:mm tt") + " and " +
                        garSubmissionSettings.EndDate.ToString("MMMM dd, yyyy hh:mm tt"));
                    return Page();
                }
            }
            var gpbEntity = await _context.GPBs
                .Include(g => g.Activities)
                .ThenInclude(a => a.Mandates)
                .FirstOrDefaultAsync(g => g.Id == gpbId);

            if (gpbEntity == null)
            {
                return NotFound();
            }

            GAR = new CreateGARViewModel
            {
                GPBId = gpbEntity.Id,
                GPB = new GPBViewModel
                {
                    Id = gpbEntity.Id,
                    Year = gpbEntity.Year,
                    TotalGAAOrBudget = gpbEntity.TotalGAAOrBudget,
                    MFOPAPOrPPA = gpbEntity.MFOPAPOrPPA,
                    ResponsibleUnit = gpbEntity.ResponsibleUnit,
                    Status = gpbEntity.Status,
                },
                GARActivities = gpbEntity.Activities
        .Where(activity => activity != null)
        .Select(activity => new GARActivityViewModel
        {
            ActivityId = activity.Id,
            ActivityName = activity.Name,
            Objective = activity.Objective,
            Budget = activity.Budget,
            ActivityType = activity.ActivityType,
            Cause = activity.Cause,
            PerformanceIndicators = activity.PerformanceIndicators,
            SourceBudget = activity.SourceBudget,
            ActualResult = string.Empty,
            ActualCost = 0
        }).ToList()
            };



            return Page();  // Render the form with the populated data
        }

        // This method handles form submission and saves the GAR entity to the database
        public async Task<IActionResult> OnPostAsync()
        {
            // Fetch submission settings for GAR
            var garSubmissionSettings = await _context.GARSubmissionSettings.FirstOrDefaultAsync();
            if (garSubmissionSettings != null)
            {
                // Check if the current date is outside the allowed submission period
                if (DateTime.Now < garSubmissionSettings.StartDate || DateTime.Now > garSubmissionSettings.EndDate)
                {
                    ModelState.AddModelError(string.Empty, "Submission is only allowed between " +
                        garSubmissionSettings.StartDate.ToString("MMMM dd, yyyy hh:mm tt") + " and " +
                        garSubmissionSettings.EndDate.ToString("MMMM dd, yyyy hh:mm tt"));
                    return Page();
                }
            }
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            var garEntity = new GAR
            {
                GPBId = GAR.GPBId,
                GARActivities = GAR.GARActivities.Select(garActivityVm => new GARActivity
                {
                    ActivityId = garActivityVm.ActivityId,
                    ActualResult = garActivityVm.ActualResult,
                    ActualCost = garActivityVm.ActualCost,
                    NatureOfEvent = garActivityVm.NatureOfEvent,
                    NumberOfParticipants = garActivityVm.NumberOfParticipants,
                    OrganizingTeamMembers = garActivityVm.OrganizingTeamMembers,

                    // Matching database file columns with code property names
                    FileNameSpecialOrder = garActivityVm.File1?.FileName,
                    ContentTypeSpecialOrder = garActivityVm.File1?.ContentType,
                    FileDataSpecialOrder = garActivityVm.File1 != null ? GetFileData(garActivityVm.File1) : null,
                    FileSizeSpecialOrder = garActivityVm.File1?.Length ?? 0,

                    FileNamePictures = garActivityVm.File2?.FileName,
                    ContentTypePictures = garActivityVm.File2?.ContentType,
                    FileDataPictures = garActivityVm.File2 != null ? GetFileData(garActivityVm.File2) : null,
                    FileSizePictures = garActivityVm.File2?.Length ?? 0,

                    FileNameBudgetReport = garActivityVm.File3?.FileName,
                    ContentTypeBudgetReport = garActivityVm.File3?.ContentType,
                    FileDataBudgetReport = garActivityVm.File3 != null ? GetFileData(garActivityVm.File3) : null,
                    FileSizeBudgetReport = garActivityVm.File3?.Length ?? 0,

                    FileNameEvaluationAttendance = garActivityVm.File4?.FileName,
                    ContentTypeEvaluationAttendance = garActivityVm.File4?.ContentType,
                    FileDataEvaluationAttendance = garActivityVm.File4 != null ? GetFileData(garActivityVm.File4) : null,
                    FileSizeEvaluationAttendance = garActivityVm.File4?.Length ?? 0,
                }).ToList()
            };


            _context.GARs.Add(garEntity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        // Helper method to save files
        // Helper method to read file data into a byte array
        private byte[] GetFileData(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

    }

    // ViewModel for GAR
    public class CreateGARViewModel
    {
        public int Id { get; set; }

        // Link to the GPB that this GAR is for
        public int GPBId { get; set; }
        public GPBViewModel GPB { get; set; }

        // Fields for GAR activities
        public List<GARActivityViewModel> GARActivities { get; set; } = new List<GARActivityViewModel>();
    }

    // ViewModel for GAR activities (Actual results to be entered by the user)
    public class GARActivityViewModel
    {
        public int Id { get; set; }

        // Reference to the corresponding GPB Activity
        public int ActivityId { get; set; }

        // Read-only fields from the GPB Activity
        public string? ActivityName { get; set; }
        public string? Objective { get; set; }
        public double? Budget { get; set; }
        // New fields to be displayed
        public string? ActivityType { get; set; }
        public string? Cause { get; set; }
        public string? PerformanceIndicators { get; set; }
        public string? SourceBudget { get; set; }


        // Fields for actual details to be entered in GAR
        [Required(ErrorMessage = "Actual result is required")]
        public string ActualResult { get; set; }

        [Required(ErrorMessage = "Actual cost is required")]
        public float ActualCost { get; set; }
        // Nature of the Event
        [Required(ErrorMessage = "Nature of Event is required.")]
        public string NatureOfEvent { get; set; }

        // Number of Participants
        [Required(ErrorMessage = "Number of Participants is required.")]
        public int NumberOfParticipants { get; set; }

        // Name of Organizing Team Members
        [Required(ErrorMessage = "Organizing Team Members are required.")]
        public string OrganizingTeamMembers { get; set; }
        // Separate file upload properties for four files
        public IFormFile File1 { get; set; }
        public IFormFile File2 { get; set; }
        public IFormFile File3 { get; set; }
        public IFormFile File4 { get; set; }
    }

    // ViewModel for GPB
    public class GPBViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Year is required")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Total GAA or Budget is required")]
        public double TotalGAAOrBudget { get; set; }

        [Required(ErrorMessage = "MFO/PAP or PPA is required")]
        public string MFOPAPOrPPA { get; set; }

        [Required(ErrorMessage = "Responsible Unit is required")]
        public string ResponsibleUnit { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        public List<ActivityViewModel> Activities { get; set; } = new List<ActivityViewModel>();
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

        [Required(ErrorMessage = "Objective is required")]
        public string Objective { get; set; }

        [Required(ErrorMessage = "Performance Indicators are required")]
        public string PerformanceIndicators { get; set; }

        [Required(ErrorMessage = "Budget is required")]
        public float Budget { get; set; }

        [Required(ErrorMessage = "Budget Source is required")]
        public string SourceBudget { get; set; }

        public List<MandateViewModel> Mandates { get; set; } = new List<MandateViewModel>();
    }

    public class MandateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mandate description is required")]
        public string Description { get; set; }
    }

}
