using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static GADApplication.Pages.AccomplishmentPage.AccomplishmentReportModel;
using static GADApplication.Pages.AccomplishmentPage.DetailsModel;

namespace GADApplication.Pages.AccomplishmentPage
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
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
                .Include(g => g.AccomplishmentReports)
                .ThenInclude(ar => ar.ActivityAccomplishments)
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
                    ActualResult = gpbEntity.AccomplishmentReports.FirstOrDefault()?.ActivityAccomplishments
                        .FirstOrDefault(ac => ac.ActivityId == activity.Id)?.ActualResult,
                    ActualCost = gpbEntity.AccomplishmentReports.FirstOrDefault()?.ActivityAccomplishments
                        .FirstOrDefault(ac => ac.ActivityId == activity.Id)?.ActualCost ?? 0,
                    NatureOfEvent = gpbEntity.AccomplishmentReports.FirstOrDefault()?.ActivityAccomplishments
                        .FirstOrDefault(ac => ac.ActivityId == activity.Id)?.NatureOfEvent,
                    NumberOfParticipants = gpbEntity.AccomplishmentReports.FirstOrDefault()?.ActivityAccomplishments
                        .FirstOrDefault(ac => ac.ActivityId == activity.Id)?.NumberOfParticipants ?? 0,
                    OrganizingTeamMembers = gpbEntity.AccomplishmentReports.FirstOrDefault()?.ActivityAccomplishments
                        .FirstOrDefault(ac => ac.ActivityId == activity.Id)?.OrganizingTeamMembers,
                    IsPdf = gpbEntity.AccomplishmentReports.FirstOrDefault()?.ActivityAccomplishments
                        .FirstOrDefault(ac => ac.ActivityId == activity.Id)?.IsPdf ?? false,
                    AccomplishmentFileName = gpbEntity.AccomplishmentReports.FirstOrDefault()?.ActivityAccomplishments
                        .FirstOrDefault(ac => ac.ActivityId == activity.Id)?.AccomplishmentFileName,
                    ContentType = gpbEntity.AccomplishmentReports.FirstOrDefault()?.ActivityAccomplishments
                        .FirstOrDefault(ac => ac.ActivityId == activity.Id)?.ContentType
                }).ToList()
            };

            return Page();
        }
    }
}
