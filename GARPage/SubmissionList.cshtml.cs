using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GADApplication.Pages.GARPage
{
    public class SubmissionListModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SubmissionListModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<GPBViewModel> GPBSubmissions { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Retrieve the list of GPB submissions with related activities and mandates
            GPBSubmissions = await _context.GPBs
                .Include(gpb => gpb.Activities)
                    .ThenInclude(activity => activity.Mandates)
                .Include(gpb => gpb.Activities)
                    .ThenInclude(activity => activity.Files)
                .Select(gpb => new GPBViewModel
                {
                    Id = gpb.Id,
                    Year = gpb.Year,
                    TotalGAAOrBudget = gpb.TotalGAAOrBudget,
                    ResponsibleUnit = gpb.ResponsibleUnit,
                    Status = gpb.Status,
                    Activities = gpb.Activities.Select(a => new ActivityViewModel
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Mandates = a.Mandates.Select(m => new MandateViewModel
                        {
                            RepublicAct = m.RepublicAct,
                            Description = m.Description
                        }).ToList(),
                        Files = a.Files.Select(f => new FileViewModel
                        {
                            Id = f.Id,
                            FileName = f.FileName,
                            UploadDate = f.UploadDate
                        }).ToList()
                    }).ToList()
                }).ToListAsync();

            return Page();
        }

        public class GPBViewModel
        {
            public int Id { get; set; }
            public int Year { get; set; }
            public double TotalGAAOrBudget { get; set; }
            public string ResponsibleUnit { get; set; }
            public string Status { get; set; }
            public List<ActivityViewModel> Activities { get; set; } = new List<ActivityViewModel>();
        }

        public class ActivityViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<MandateViewModel> Mandates { get; set; } = new List<MandateViewModel>();
            public List<FileViewModel> Files { get; set; } = new List<FileViewModel>();
        }

        public class MandateViewModel
        {
            public string RepublicAct { get; set; }
            public string Description { get; set; }
        }

        public class FileViewModel
        {
            public int Id { get; set; }
            public string FileName { get; set; }
            public DateTime UploadDate { get; set; }
        }
    }
}
