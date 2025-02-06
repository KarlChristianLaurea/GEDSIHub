using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using GADApplication.Data;
using GADApplication.Identity;
using GADApplication.Models;

public class NotificationService
{
    private readonly IEmailSender _emailSender;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public NotificationService(
        IEmailSender emailSender,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext dbContext)
    {
        _emailSender = emailSender;
        _userManager = userManager;
        _dbContext = dbContext;
    }

    // Method to send an approval email and save a notification to the database
    public async Task SendApprovalEmailAsync(ApplicationUser user, string approvalStatus, string adminComments, GPB gpb)
    {
        string subject = $"Your GPB Submission has been {approvalStatus}";
        string message = $"Dear {user.UserName},<br><br>" +
                         $"Your submitted GPB for the year {gpb.Year} has been {approvalStatus}.<br>" +
                         $"Comments from the admin: {adminComments ?? "No comments provided."}<br><br>" +
                         "Please log in to the system for further details.<br><br>" +
                         "Best regards,<br>YourApp Team";

        await _emailSender.SendEmailAsync(user.Email, subject, message);

        // Create a new notification for the user
        var notification = new Notification
        {
            SenderId = user.Id,
            ReceiverId = user.Id,
            Message = $"Your GPB has been {approvalStatus}. Please review the comments.",
            CreatedAt = DateTime.UtcNow,
            IsRead = false,
            IsAdminNotification = true
        };

        // Add the notification to the database
        _dbContext.Notifications.Add(notification);
        await _dbContext.SaveChangesAsync();
    }

    // Method to notify all users about a new calendar event
    public async Task NotifyAllUsersAboutEventAsync(string eventName, string eventLocation, DateTime start, DateTime end)
    {
        var allUsers = await _userManager.Users.ToListAsync();
        string subject = "New Event Added ";
        string message = $"A new event '{eventName}' has been added.<br>" +
                         $"Location: {eventLocation}<br>" +
                         $"Start: {start}<br>" +
                         $"End: {end}<br>" +
                         "Please log in to the system for more details.<br><br>" +
                         "Best regards,<br>GADO GEDSIHUB";

        foreach (var user in allUsers)
        {
            // Send email to each user
            await _emailSender.SendEmailAsync(user.Email, subject, message);

            // Create a notification for each user
            var notification = new Notification
            {
                SenderId = user.Id,
                ReceiverId = user.Id,
                Message = $"A new event '{eventName}' has been added. Check the GEDSIHub for more details.",
                CreatedAt = DateTime.UtcNow,
                IsRead = false,
                IsAdminNotification = true
            };

            // Add the notification to the context
            _dbContext.Notifications.Add(notification);
        }

        // Save all notifications to the database
        await _dbContext.SaveChangesAsync();
    }

    // Method to send a custom notification to a specific user
    public async Task SendCustomNotificationAsync(ApplicationUser user, string message)
    {
        var notification = new Notification
        {
            SenderId = user.Id,
            ReceiverId = user.Id,
            Message = message,
            CreatedAt = DateTime.UtcNow,
            IsRead = false,
            IsAdminNotification = false
        };

        // Add the notification to the database
        _dbContext.Notifications.Add(notification);
        await _dbContext.SaveChangesAsync();

        // Send an email to the user as well
        string subject = "New Notification";
        await _emailSender.SendEmailAsync(user.Email, subject, message);
    }
}
