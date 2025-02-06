using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GADApplication.Pages.GARPage
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
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
