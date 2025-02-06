using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using GADApplication.Models;
using GADApplication.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GADApplication.Identity;

namespace GADApplication.Pages
{
    public class AdminInboxModel : PageModel
    {
        private readonly MessageService _messageService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminInboxModel(MessageService messageService, UserManager<ApplicationUser> userManager)
        {
            _messageService = messageService;
            _userManager = userManager;
        }

        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public List<Message> Messages { get; set; } = new List<Message>();

        // Load the inbox for admin
        public async Task OnGetAsync()
        {
            var adminEmail = "admin@example.com"; // Admin's email
            var usersWhoMessagedAdmin = await _messageService.GetAllMessagesAsync(); // Get all messages

            // Extract users who have messaged the admin
            var userEmails = usersWhoMessagedAdmin
                .Where(m => m.Sender == adminEmail || m.Receiver == adminEmail)
                .Select(m => m.Sender == adminEmail ? m.Receiver : m.Sender)
                .Distinct();

            // Fetch the users who sent/received messages from/to admin
            Users = await _userManager.Users
                .Where(u => userEmails.Contains(u.Email))
                .ToListAsync();

            // Set last message preview for each user
            foreach (var user in Users)
            {
                var lastMessage = await _messageService.GetLastMessageAsync(user.Email);
                user.LastMessagePreview = lastMessage?.Content ?? "No messages yet";
            }
        }

        // Load messages for the selected user (admin-specific)
        public async Task<IActionResult> OnGetLoadUserMessagesAsync(string conversationUser)
        {
            if (!string.IsNullOrEmpty(conversationUser))
            {
                var adminEmail = "admin@example.com";
                var messages = await _messageService.GetConversationAsync(adminEmail, conversationUser);

                // Log the count of retrieved messages for debugging
                Console.WriteLine($"Loaded {messages.Count} messages for conversation with {conversationUser}");

                foreach (var message in messages)
                {
                    Console.WriteLine($"Message from {message.Sender} to {message.Receiver}: {message.Content}");
                }

                return new JsonResult(messages);
            }

            Console.WriteLine("No conversation user provided.");
            return new JsonResult(new List<Message>());
        }


    }
}