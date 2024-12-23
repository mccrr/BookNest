using BookNest.Dtos.Notifications;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Notification
    {
        public  Notification() { }
        public Notification(NotificationDto dto)
        {
            UserId = dto.UserId;
            Text = dto.Text;
            BookId = dto.BookId;
        }
        [Key]
        public int Id { get; set; }
        [Required] 
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string BookId { get; set; }
        public Book Book { get; set; }
        [Required]
        [StringLength(250)]
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<UserNotification>? UserNotifications { get; set; }

    }
}
