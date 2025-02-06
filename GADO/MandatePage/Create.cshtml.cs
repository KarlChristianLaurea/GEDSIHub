using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GADApplication.Data;
using GADApplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace GADApplication.Pages.GADO.MandatePage
{

    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        
        private readonly GADApplication.Data.ApplicationDbContext _context;

        public CreateModel(GADApplication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ActivityId"] = new SelectList(_context.Activities, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Mandate Mandate { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Mandates.Add(Mandate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
