using GADApplication.Data;
using GADApplication.Models;
using GADApplication.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GADApplication.Services
{
    public class MessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Save the message between sender and receiver
        public async Task SaveMessageAsync(string senderEmail, string receiverEmail, string content)
        {
            var message = new Message
            {
                Sender = senderEmail,
                Receiver = receiverEmail,
                Content = content,
                Timestamp = DateTime.Now // Ensure timestamp is set here
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        // Retrieve conversation between admin and a specific user using UserManager
        public async Task<List<Message>> GetConversationAsync(string adminEmail, string userEmail)
        {
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (adminUser == null || user == null)
            {
                return new List<Message>(); // Return empty list if users are not found
            }

            return await _context.Messages
                .Where(m => (m.Sender == adminEmail && m.Receiver == userEmail) ||
                            (m.Sender == userEmail && m.Receiver == adminEmail))
                .OrderBy(m => m.Timestamp)  // Order messages by timestamp to display in chronological order
                .ToListAsync();
        }

        // Retrieve the last message for a user using UserManager
        public async Task<Message> GetLastMessageAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null; // Return null if user is not found
            }

            return await _context.Messages
                .Where(m => m.Receiver == email || m.Sender == email)
                .OrderByDescending(m => m.Timestamp)
                .FirstOrDefaultAsync();
        }

        // Retrieve all messages for the admin using UserManager
        public async Task<List<Message>> GetAllMessagesAsync()
        {
            return await _context.Messages
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }
    }
}
