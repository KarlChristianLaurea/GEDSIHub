using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GADApplication.Data;
using Microsoft.AspNetCore.Identity;
using GADApplication.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using GADApplication.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GADApplication.Pages
{
    [Authorize]
    public class NotificationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Notification> UserNotifications { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            // Fetch notifications for the current user and admin notifications if user is an admin.
            UserNotifications = await _context.Notifications
                .Where(n => n.ReceiverId == user.Id || (n.IsAdminNotification && User.IsInRole("Admin")))
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostMarkAsReadAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            var user = await _userManager.GetUserAsync(User);

            if (notification != null && notification.ReceiverId == user.Id)
            {
                notification.IsRead = true;
                _context.Notifications.Update(notification);
                await _context.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }

            return new JsonResult(new { success = false });
        }
    }
}
