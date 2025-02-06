using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GADApplication.Pages.GARPage
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int GPBId { get; set; }
        public List<FileEntity> UploadedFiles { get; set; }

        public async Task OnGetAsync(int gpbId)
        {
            GPBId = gpbId;

            UploadedFiles = await _context.FileEntities
                .Include(f => f.Activity)
                .Where(f => f.Activity.GPBId == GPBId)
                .ToListAsync();
        }
    }
}
