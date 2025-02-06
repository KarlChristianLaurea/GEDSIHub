using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IronPdf;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GADApplication.Pages.GARPage
{
    public class DownloadGARModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DownloadGARModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<GPB> GPBs { get; set; }
        public Dictionary<int, List<FileEntity>> ActivityFiles { get; set; } = new Dictionary<int, List<FileEntity>>();
        public string YearFilter { get; set; }
        public string UnitFilter { get; set; }
        public string ActivityNameFilter { get; set; }
        public string StatusFilter { get; set; }
        public List<string> ResponsibleUnits { get; set; }
        public List<int> SubmissionYears { get; set; }

        public async Task<IActionResult> OnGetAsync(string year, string responsibleUnit, string activityName)
        {
            YearFilter = year;
            UnitFilter = responsibleUnit;
            ActivityNameFilter = activityName;

            ResponsibleUnits = await _context.GPBs
                .Where(g => g.Status == "Approved with Files" || g.Status == "Rejected with Files")
                .Select(g => g.ResponsibleUnit)
                .Distinct()
                .ToListAsync();

            SubmissionYears = await _context.GPBs
                .Where(g => g.Status == "Approved with Files" || g.Status == "Rejected with Files")
                .Select(g => g.Year)
                .Distinct()
                .OrderByDescending(y => y)
                .ToListAsync();

            IQueryable<GPB> query = _context.GPBs
                .Include(g => g.Activities)
                    .ThenInclude(a => a.Mandates)
                .Include(g => g.Activities)
                    .ThenInclude(a => a.Files)
                .Where(g => g.Status == "Approved with Files" || g.Status == "Rejected with Files");

            // Apply additional filters based on user input
            if (!string.IsNullOrEmpty(year))
                query = query.Where(g => g.Year.ToString() == year);

            if (!string.IsNullOrEmpty(responsibleUnit))
                query = query.Where(g => g.ResponsibleUnit == responsibleUnit);

            if (!string.IsNullOrEmpty(activityName))
                query = query.Where(g => g.Activities.Any(a => a.Name.Contains(activityName)));

            GPBs = await query.ToListAsync();

            // Load files for activities
            foreach (var gpb in GPBs)
            {
                foreach (var activity in gpb.Activities)
                {
                    var files = await _context.FileEntities
                        .Where(f => f.ActivityId == activity.Id)
                        .OrderByDescending(f => f.UploadDate)
                        .ToListAsync();
                    ActivityFiles[activity.Id] = files;
                }
            }

            return Page();
        }


        public async Task<IActionResult> OnPostDownloadPdfAsync(string year, string responsibleUnit, string activityName)
        {
            IQueryable<GPB> query = _context.GPBs
                .Include(g => g.Activities)
                    .ThenInclude(a => a.Mandates)
                .Include(g => g.Activities)
                    .ThenInclude(a => a.Files)
                .Where(g => g.Status == "Approved with Files" || g.Status == "Rejected with Files");

            // Apply filters
            if (!string.IsNullOrEmpty(year))
                query = query.Where(g => g.Year.ToString() == year);

            if (!string.IsNullOrEmpty(responsibleUnit))
                query = query.Where(g => g.ResponsibleUnit == responsibleUnit);

            if (!string.IsNullOrEmpty(activityName))
                query = query.Where(g => g.Activities.Any(a => a.Name.Contains(activityName)));

            GPBs = await query.ToListAsync();

            if (GPBs == null || !GPBs.Any())
            {
                TempData["ErrorMessage"] = "No GPB submissions match the selected filters.";
                return RedirectToPage();
            }

            string htmlContent = GeneratePdfContent(GPBs);

            // Configure IronPdf renderer for narrow margins
            var renderer = new ChromePdfRenderer();
            renderer.RenderingOptions.PaperOrientation = IronPdf.Rendering.PdfPaperOrientation.Landscape;

            // Set paper size to A4 and narrow margins
            renderer.RenderingOptions.MarginTop = 1;    // Top margin in millimeters
            renderer.RenderingOptions.MarginBottom = 1; // Bottom margin in millimeters
            renderer.RenderingOptions.MarginLeft = 1;   // Left margin in millimeters
            renderer.RenderingOptions.MarginRight = 1;  // Right margin in millimeters

            var pdfDoc = renderer.RenderHtmlAsPdf(htmlContent);
            return File(pdfDoc.BinaryData, "application/pdf", $"Filtered_GPB_{DateTime.Now:yyyyMMdd}.pdf");
        }




        private string GeneratePdfContent(IList<GPB> gpbs)
        {
            double totalBudget = gpbs.Sum(g => g.TotalGAAOrBudget);
            int totalActivities = gpbs.Sum(g => g.Activities.Count);

            // Calculate total actual cost across all FileEntities
            double totalActualCost = gpbs
                .SelectMany(g => g.Activities)
                .SelectMany(a => a.Files)
                .Where(f => f.ActualCost.HasValue)
                .Sum(f => f.ActualCost.Value);

            // Calculate utilization rate
            double utilizationRate = (totalActualCost / totalBudget) * 100;
            string utilizationStatus = utilizationRate >= 100 ? "Positive" : "Negative";

            // Gather year and MFOPAP
            var years = gpbs.Select(g => g.Year).Distinct().OrderByDescending(y => y).ToList();
            var mfopaps = gpbs.Select(g => g.MFOPAPOrPPA).Distinct().ToList();

            string content = $@"
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; }}
        h1 {{ text-align: center; color: #2C3E50; }}
        .summary-section, .table-section {{ width: 100%; margin-top: 20px; }}
        .summary-section table {{ width: 100%; border-collapse: collapse; margin-bottom: 20px; }}
        .summary-section th, .summary-section td {{ padding: 8px; text-align: left; }}
        .table-section table {{ width: 100%; border-collapse: collapse; }}
        th, td {{ border: 1px solid #ddd; padding: 8px; text-align: center; }}
        th {{ background-color: #f2f2f2; }}
        .header-logo {{ text-align: center; margin-bottom: 20px; }}
        ul {{ margin: 0; padding-left: 15px; }}
    </style>
</head>
<body>
    <div class='header-logo'>
        <img src='/images/GEDSI Hub logo.png' alt='Logo' style='height: 80px;' />
    </div>

    <div class='summary-section'>
        <table>
            <tr>
                <th>Organization</th>
                <td>Polytechnic University of the Philippines</td>
            </tr>
            <tr>
                <th>Total Budget</th>
                <td>{totalBudget:C}</td>
            </tr>
            <tr>
                <th>Total GAD Budget</th>
                <td>{gpbs.Sum(g => g.TotalGAAOrBudget):C}</td>
            </tr>
            <tr>
                <th>Number of Activities</th>
                <td>{totalActivities}</td>
            </tr>
            <tr>
                <th>Total Actual Cost</th>
                <td>{totalActualCost:C}</td>
            </tr>
            <tr>
                <th>Utilization Rate</th>
                <td>{utilizationRate:F2}% ({utilizationStatus} Utilization)</td>
            </tr>
            <tr>
                <th>Submission Years</th>
                <td>{string.Join(", ", years)}</td>
            </tr>
            <tr>
                <th>MFOPAP</th>
                <td>{string.Join("<br>", mfopaps)}</td>
            </tr>
        </table>
    </div>";

            // Function to generate a section for each activity type
            string GenerateActivityTable(IEnumerable<Activity> activities, string type)
            {
                if (!activities.Any()) return string.Empty;

                string section = $@"
    <h2>{type} Activities</h2>
    <div class='table-section'>
        <table>
            <thead>
                <tr>
                    <th>Total Budget</th>
                    <th>Responsible Unit</th>
                    <th>Gender Issue/GAD Mandate</th>
                    <th>Cause of Gender Issue</th>
                    <th>GAD Result Statement/GAD Objective</th>
                    <th>GAD Activity</th>
                    <th>Performance Indicators/Targets</th>
                    <th>GAD Budget</th>
                    <th>Source of Budget</th>
                    <th>Files - Actual Results</th>
                    <th>Files - Actual Costs</th>
                    <th>Files - Nature of Events</th>
                    <th>Files - Number of Participants</th>
                    <th>Files - Organizing Team Members</th>
                </tr>
            </thead>
            <tbody>";

                foreach (var activity in activities)
                {
                    string mandates = string.Join("<br>", activity.Mandates.Select(m => $"{m.RepublicAct} - {m.Description}"));
                    var gpb = activity.GPB;

                    // Consolidate file details into a single cell
                    string fileResults = string.Join("<br>", activity.Files.Select(f => f.ActualResult ?? "N/A"));
                    string fileCosts = string.Join("<br>", activity.Files.Select(f => f.ActualCost?.ToString("C") ?? "N/A"));
                    string fileEvents = string.Join("<br>", activity.Files.Select(f => f.NatureOfEvent ?? "N/A"));
                    string fileParticipants = string.Join("<br>", activity.Files.Select(f => f.NumberOfParticipants?.ToString() ?? "N/A"));
                    string fileTeamMembers = string.Join("<br>", activity.Files.Select(f => f.OrganizingTeamMembers ?? "N/A"));

                    section += $@"
        <tr>
            <td>{gpb.TotalGAAOrBudget:C}</td>
            <td>{gpb.ResponsibleUnit}</td>
            <td>{mandates}</td>
            <td>{activity.Cause}</td>
            <td>{activity.Objective}</td>
            <td>{activity.Name}</td>
            <td>{activity.PerformanceIndicators}</td>
            <td>{activity.Budget:C}</td>
            <td>{activity.SourceBudget}</td>
            <td>{fileResults}</td>
            <td>{fileCosts}</td>
            <td>{fileEvents}</td>
            <td>{fileParticipants}</td>
            <td>{fileTeamMembers}</td>
        </tr>";
                }

                section += @"
        </tbody>
    </table>
</div>";

                return section;
            }


            // Generate sections for each type of activity based on dropdown values
            content += GenerateActivityTable(gpbs.SelectMany(g => g.Activities).Where(a => a.ActivityType == "Client-Focused"), "Client-Focused");
            content += GenerateActivityTable(gpbs.SelectMany(g => g.Activities).Where(a => a.ActivityType == "Attributed_Based"), "Attributed Based");
            content += GenerateActivityTable(gpbs.SelectMany(g => g.Activities).Where(a => a.ActivityType == "Organization-Focused"), "Organization-Focused");

            content += @"
    <div class='footer'>
        <p style='text-align:center; margin-top: 20px;'>Generated on: " + DateTime.Now.ToString("MMMM dd, yyyy") + @"</p>
    </div>
</body>
</html>";

            return content;
        }

        // Handler for file download
        public async Task<IActionResult> OnGetDownloadFileAsync(int fileId)
        {
            var file = await _context.FileEntities.FindAsync(fileId);
            if (file == null) return NotFound("File not found.");

            Response.Headers.Add("Content-Disposition", $"attachment; filename={file.FileName}");
            return File(file.FileData, file.ContentType);
        }

        // Handler for file preview
        public async Task<IActionResult> OnGetPreviewFileAsync(int fileId)
        {
            var file = await _context.FileEntities.FindAsync(fileId);
            if (file == null) return NotFound("File not found.");

            if (file.ContentType == "application/pdf")
            {
                Response.Headers.Add("Content-Disposition", "inline; filename=" + file.FileName);
            }
            else
            {
                Response.Headers.Add("Content-Disposition", "attachment; filename=" + file.FileName);
            }

            return File(file.FileData, file.ContentType);
        }
    }
}
