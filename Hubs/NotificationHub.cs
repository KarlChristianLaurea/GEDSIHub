
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GADApplication.Hubs;
public class NotificationHub : Hub
{
    // This method will be used to send notifications to a specific user
    public async Task SendNotification(string userId, string message)
    {
        // Sends a notification to the specific user (using their user ID)
        await Clients.User(userId).SendAsync("ReceiveNotification", message);
    }

    // Optional: If you want to notify all connected users
    public async Task BroadcastNotification(string message)
    {
        await Clients.All.SendAsync("ReceiveNotification", message);
    }
}
