using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Notification
    {
        public  Notification() { }
        [Key]
        public int Id { get; set; }
        [Required] 
        public required int UserId { get; set; }
        public required User User { get; set; }
        [Required]
        public required string BookId { get; set; }
        public required Book Book { get; set; }
        [Required]
        [StringLength(250)]
        public required string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<UserNotification>? UserNotifications { get; set; }

    }
}
