using BookNest.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos.Notifications
{
    public class NotificationDto
    {
        public NotificationDto(int userId, int otherId, string bookId, string type, string text) {
            UserId = userId;
            OtherId = otherId;
            BookId = bookId;
            Type = type;
            Text = text;
        }
        public int UserId { get; set; }
        public string BookId { get; set; }
        public string Text { get; set; }
        public int OtherId { get; set; }
        public string Type { get; set; }
    }
}
