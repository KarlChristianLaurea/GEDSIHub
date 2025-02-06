using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GADApplication.Pages.GPBPage
{
    public class GPBProgressModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationIdentityDbContext _identityContext;

        public GPBProgressModel(ApplicationDbContext context, ApplicationIdentityDbContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }

        public int TotalUnits { get; set; }
        public int SubmittedReports { get; set; }
        public double SubmissionProgress { get; set; } // Holds the percentage value
        public List<GPB> GPBList { get; set; }

        private int GetStatusOrder(string approvalStatus)
        {
            return approvalStatus switch
            {
                "Draft" => 0,
                "Pending" => 1,
                "Rejected - Edit and Resubmit" => 2,
                "Approved and Ready for File Upload" => 3,
                "Rejected with Files" => 4,
                "Approved with Files" => 5,
                _ => 6  // Any other status or blanks come last
            };
        }

        public async Task OnGetAsync()
        {
            // Fetch the total number of Responsible Units
            TotalUnits = await _identityContext.ResponsibleUnits.CountAsync();

            // Define statuses that are considered as submitted
            var submittedStatuses = new List<string>
            {
                "Pending",
                "Rejected - Edit and Resubmit",
                "Approved and Ready for File Upload",
                "Rejected with Files",
                "Approved with Files"
            };

            // Count the number of GPBs that have been submitted (status beyond 'Draft')
            SubmittedReports = await _context.GPBs.CountAsync(gpb => submittedStatuses.Contains(gpb.ApprovalStatus));

            // Calculate the percentage of completed reports
            SubmissionProgress = TotalUnits > 0 ? (double)SubmittedReports / TotalUnits : 0;

            // Fetch and sort the GPBs
            GPBList = await _context.GPBs.ToListAsync();

            GPBList = GPBList
                .OrderBy(gpb => GetStatusOrder(gpb.ApprovalStatus))
                .ThenBy(gpb => gpb.Year)
                .ToList();
        }

        // Calculate the progress percentage based on approval status
        public int GetProgressPercentage(string approvalStatus)
        {
            return approvalStatus switch
            {
                "Draft" => 0,
                "Pending" => 20,
                "Rejected - Edit and Resubmit" => 40,
                "Approved and Ready for File Upload" => 60,
                "Rejected with Files" => 80,
                "Approved with Files" => 100,
                _ => 0  // Default for any unknown status
            };
        }

        // Get color for the progress bar based on status
        // Get color for the progress bar based on status
        // Get color for the progress bar based on status
        public string GetProgressBarColor(string approvalStatus)
        {
            return approvalStatus switch
            {
                "Draft" => "bg-secondary",
                "Pending" => "bg-warning",  // Yellow for Pending
                "Rejected - Edit and Resubmit" => "bg-warning",
                "Approved and Ready for File Upload" => "bg-orange",  // Orange for Approved and Ready for File Upload
                "Rejected with Files" => "bg-danger",
                "Approved with Files" => "bg-success",  // Green for Approved with Files
                _ => "bg-light"  // Default color for any unknown status
            };
        }


    }
}