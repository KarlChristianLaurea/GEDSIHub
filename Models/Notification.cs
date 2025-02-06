namespace GADApplication.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string SenderId { get; set; } // Id of the sender (Admin or User)
        public string ReceiverId { get; set; } // Id of the receiver (Admin or User)
        public string Message { get; set; }
        public string? Role { get; set; } // Role of the receiver (admin/user)
        public bool IsRead { get; set; } = false; // To track whether the user/admin has read the notification
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Timestamp for notification creation
        public bool IsAdminNotification { get; set; }
    }
}
