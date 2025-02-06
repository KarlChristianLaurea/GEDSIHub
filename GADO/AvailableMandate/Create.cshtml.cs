using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GADApplication.Data;
using GADApplication.Models;

namespace GADApplication.Pages.GADO.AvailableMandate
{
    public class CreateModel : PageModel
    {
        private readonly GADApplication.Data.ApplicationDbContext _context;

        public CreateModel(GADApplication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AvailableMandates AvailableMandates { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AvailableMandates.Add(AvailableMandates);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
