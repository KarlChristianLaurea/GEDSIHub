using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IronPdf;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace GADApplication.Pages.GPBPage
{
    public class FilterAndDownloadGPBModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public FilterAndDownloadGPBModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<GPB> GPBs { get; set; }
        public string YearFilter { get; set; }
        public string UnitFilter { get; set; }
        public string ActivityNameFilter { get; set; }
        public List<string> ResponsibleUnits { get; set; }
        public List<int> SubmissionYears { get; set; }

        public async Task<IActionResult> OnGetAsync(string year, string responsibleUnit, string activityName)
        {
            YearFilter = year;
            UnitFilter = responsibleUnit;
            ActivityNameFilter = activityName;

            ResponsibleUnits = await _context.GPBs.Select(g => g.ResponsibleUnit).Distinct().ToListAsync();
            SubmissionYears = await _context.GPBs.Select(g => g.Year).Distinct().OrderByDescending(y => y).ToListAsync();

            IQueryable<GPB> query = _context.GPBs
                .Include(g => g.Activities)
                .ThenInclude(a => a.Mandates);

            if (!string.IsNullOrEmpty(year))
            {
                query = query.Where(g => g.Year.ToString() == year);
            }

            if (!string.IsNullOrEmpty(responsibleUnit))
            {
                query = query.Where(g => g.ResponsibleUnit == responsibleUnit);
            }

            if (!string.IsNullOrEmpty(activityName))
            {
                query = query.Where(g => g.Activities.Any(a => a.Name.Contains(activityName)));
            }

            GPBs = await query.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostDownloadPdfAsync(string year, string responsibleUnit, string activityName)
        {
            IQueryable<GPB> query = _context.GPBs
                .Include(g => g.Activities)
                .ThenInclude(a => a.Mandates);

            if (!string.IsNullOrEmpty(year))
            {
                query = query.Where(g => g.Year.ToString() == year);
            }

            if (!string.IsNullOrEmpty(responsibleUnit))
            {
                query = query.Where(g => g.ResponsibleUnit == responsibleUnit);
            }

            if (!string.IsNullOrEmpty(activityName))
            {
                query = query.Where(g => g.Activities.Any(a => a.Name.Contains(activityName)));
            }

            GPBs = await query.ToListAsync();

            if (GPBs == null || GPBs.Count == 0)
            {
                TempData["ErrorMessage"] = "No GPB submissions to download.";
                return RedirectToPage();
            }

            string htmlContent = GeneratePdfContent(GPBs, year);

            // Initialize IronPDF renderer and set A4 landscape size
            var renderer = new ChromePdfRenderer();
            renderer.RenderingOptions.PaperSize = IronPdf.Rendering.PdfPaperSize.A4;
            renderer.RenderingOptions.PaperOrientation = IronPdf.Rendering.PdfPaperOrientation.Landscape;
            renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Screen;

            var pdfDoc = renderer.RenderHtmlAsPdf(htmlContent);

            byte[] pdf = pdfDoc.BinaryData;

            return File(pdf, "application/pdf", $"GPB_Submissions_{year}_{responsibleUnit}_{activityName}.pdf");
        }

        private string GeneratePdfContent(IList<GPB> gpbs, string year)
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
    <h1>GPB Submissions for Year {year}</h1>

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