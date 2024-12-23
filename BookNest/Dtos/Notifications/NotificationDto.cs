using BookNest.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos.Notifications
{
    public class NotificationDto
    {
        public int UserId { get; set; }
        public string BookId { get; set; }
        public string Text { get; set; }
    }
}
