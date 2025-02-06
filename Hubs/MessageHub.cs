using GADApplication.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GADApplication.Hubs
{
    public class MessageHub : Hub
    {
        private readonly MessageService _messageService;

        public MessageHub(MessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task SendMessage(string sender, string receiver, string message)
        {
            // Validate that the message is not empty or null
            if (string.IsNullOrWhiteSpace(message))
            {
                // Optionally send a message back to the sender indicating invalid input
                await Clients.Caller.SendAsync("ReceiveMessage", "System", "Message cannot be empty.");
                return;
            }

            // Try sending the message to the receiver
            try
            {
                var timestamp = DateTime.UtcNow.ToString("o"); // Format timestamp in ISO 8601

                // Send the message to the receiver in real-time with a timestamp
                await Clients.User(receiver).SendAsync("ReceiveMessage", sender, message, timestamp);

                // Also update the sender's UI in real-time to reflect the sent message with a timestamp
                await Clients.Caller.SendAsync("ReceiveMessage", sender, message, timestamp);

                // Save the message to the database after successfully sending it
                await _messageService.SaveMessageAsync(sender, receiver, message);
            }
            catch (System.Exception ex)
            {
                // Log the error (optional)
                // _logger.LogError("Error sending message", ex);

                // Inform the sender that the message couldn't be sent
                await Clients.Caller.SendAsync("ReceiveMessage", "System", "Failed to send message. Please try again.");
            }
        }
    }
}
