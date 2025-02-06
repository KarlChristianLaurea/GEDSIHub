using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using GADApplication.Identity; // Include your namespace

public class FPSUserListModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public FPSUserListModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    public async Task OnGetAsync()
    {
        Users = await _userManager.Users
                .Where(u => u.Id != null && u.UserName != null) // Ensure non-null users
                .ToListAsync();
    }
}
    