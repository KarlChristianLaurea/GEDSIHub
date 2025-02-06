using System;

namespace GADApplication.Models
{
    public class Message
    {
        public int Id { get; set; } // Primary key

        public string Sender { get; set; } // User who sends the message

        public string Receiver { get; set; } // User who receives the message

        public string Content { get; set; } // The actual message content

        public DateTime Timestamp { get; set; } // Time when the message was sent
    }
}
