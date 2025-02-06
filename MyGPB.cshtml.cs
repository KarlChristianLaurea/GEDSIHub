using GADApplication.Data;
using GADApplication.Models;
using GADApplication.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IronPdf;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GADApplication.Pages.MyGPB
{
    public class MyGPBModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyGPBModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<GPB> GPBs { get; set; }
        public string UserResponsibleUnit { get; set; }
        public string SearchActivity { get; set; }
        public int TotalActivities { get; set; }
        public double TotalBudget { get; set; }


        public async Task<IActionResult> OnGetAsync(string searchActivity)
        {
            SearchActivity = searchActivity;

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found. Please log in again.";
                return RedirectToPage("/Account/Login");
            }

            UserResponsibleUnit = user.ResponsibleUnit;

            if (string.IsNullOrEmpty(UserResponsibleUnit))
            {
                TempData["ErrorMessage"] = "No responsible unit associated with your account.";
                return Page();
            }

            var query = _context.GPBs
                .Where(g => EF.Functions.Like(g.ResponsibleUnit.Trim().ToLower(), UserResponsibleUnit.Trim().ToLower()))
                .Include(g => g.Activities)
                .ThenInclude(a => a.Mandates)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchActivity))
            {
                query = query.Where(g => g.Activities.Any(a => EF.Functions.Like(a.Name, $"%{SearchActivity}%")));
            }

            GPBs = await query.ToListAsync();

            // Summary for filtered data
            TotalBudget = GPBs.Sum(g => g.TotalGAAOrBudget);
            TotalActivities = GPBs.Sum(g => g.Activities.Count);

            if (GPBs == null || GPBs.Count == 0)
            {
                TempData["ErrorMessage"] = "No GPB submissions found for your responsible unit.";
            }

            return Page();
        }


        public async Task<IActionResult> OnPostDownloadPdfAsync(string searchActivity)
        {
            // Use the same filtering logic as OnGetAsync
            SearchActivity = searchActivity;

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found. Please log in again.";
                return RedirectToPage("/Account/Login");
            }

            UserResponsibleUnit = user.ResponsibleUnit;

            if (string.IsNullOrEmpty(UserResponsibleUnit))
            {
                TempData["ErrorMessage"] = "No responsible unit associated with your account.";
                return RedirectToPage();
            }

            var query = _context.GPBs
                .Where(g => EF.Functions.Like(g.ResponsibleUnit.Trim().ToLower(), UserResponsibleUnit.Trim().ToLower()))
                .Include(g => g.Activities)
                .ThenInclude(a => a.Mandates)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchActivity))
            {
                query = query.Where(g => g.Activities.Any(a => EF.Functions.Like(a.Name, $"%{SearchActivity}%")));
            }

            GPBs = await query.ToListAsync();

            // Verify there are results
            if (GPBs == null || GPBs.Count == 0)
            {
                TempData["ErrorMessage"] = "No GPB submissions to download.";
                return RedirectToPage();
            }

            // Generate the PDF content
            string htmlContent = GeneratePdfContent(GPBs);
            var renderer = new ChromePdfRenderer();
            renderer.RenderingOptions.PaperSize = IronPdf.Rendering.PdfPaperSize.A4;
            renderer.RenderingOptions.PaperOrientation = IronPdf.Rendering.PdfPaperOrientation.Landscape;

            var pdfDoc = renderer.RenderHtmlAsPdf(htmlContent);
            byte[] pdf = pdfDoc.BinaryData;

            return File(pdf, "application/pdf", $"My_GPB_Submissions.pdf");
        }




        private string GeneratePdfContent(IList<GPB> gpbs)
        {
            double totalBudget = gpbs.Sum(g => g.TotalGAAOrBudget);
            int totalActivities = gpbs.Sum(g => g.Activities.Count);

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
    <h1>GPB Submissions for Year </h1>

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
                <td>{totalBudget:C}</td>
            </tr>
            <tr>
                <th>Number of Activities</th>
                <td>{totalActivities}</td>
            </tr>
        </table>
    </div>";

            // Generate sections for each activity type
            content += GenerateActivityTable(gpbs, "Client-Focused");
            content += GenerateActivityTable(gpbs, "Attributed_Based");
            content += GenerateActivityTable(gpbs, "Organization-Focused");

            content += @"
    <div class='footer'>
        <p style='text-align:center; margin-top: 20px;'>Generated on: " + DateTime.Now.ToString("MMMM dd, yyyy") + @"</p>
    </div>
</body>
</html>";

            return content;
        }

        private string GenerateActivityTable(IEnumerable<GPB> gpbs, string activityType)
        {
            var activities = gpbs.SelectMany(g => g.Activities).Where(a => a.ActivityType == activityType);

            if (!activities.Any())
                return string.Empty;

            string section = $@"
<h2>{activityType} Activities</h2>
<div class='table-section'>
    <table>
        <thead>
            <tr>
                <th>Year</th>
                <th>Total Budget</th>
                <th>Responsible Unit</th>
                <th>Status</th>
                <th>Gender Issue/GAD Mandate</th>
                <th>Cause of Gender Issue</th>
                <th>GAD Result Statement/GAD Objective</th>
                <th>Relevant Organization MFO/PAP or PPA</th>
                <th>GAD Activity</th>
                <th>Performance Indicators/Targets</th>
                <th>GAD Budget</th>
                <th>Source of Budget</th>
                <th>Responsible Unit/Office</th>
            </tr>
        </thead>
        <tbody>";

            foreach (var activity in activities)
            {
                string mandates = string.Join("<br>", activity.Mandates.Select(m => $"{m.RepublicAct} - {m.Description}"));
                var gpb = activity.GPB;

                section += $@"
        <tr>
            <td>{gpb.Year}</td>
            <td>{gpb.TotalGAAOrBudget:C}</td>
            <td>{gpb.ResponsibleUnit}</td>
            <td>{gpb.Status}</td>
            <td>{mandates}</td>
            <td>{activity.Cause}</td>
            <td>{activity.Objective}</td>
            <td>{gpb.MFOPAPOrPPA}</td>
            <td>{activity.Name}</td>
            <td>{activity.PerformanceIndicators}</td>
            <td>{activity.Budget:C}</td>
            <td>{activity.SourceBudget}</td>
            <td>{gpb.ResponsibleUnit}</td>
        </tr>";
            }

            section += @"
    </tbody>
</table>
</div>";

            return section;
        }

    }
}
