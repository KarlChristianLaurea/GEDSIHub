using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using GADApplication.Identity; // Include your namespace

public class UserListModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserListModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public List<ApplicationUser> Users { get; set; }

    public async Task OnGetAsync()
    {
        Users = await _userManager.Users
                .Where(u => u.Id != null && u.UserName != null) // Ensure non-null users
                .ToListAsync();
    }
}
