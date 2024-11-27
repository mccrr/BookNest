namespace BookNest.Models.Entities
{
    public class UserNotification
    {
        public UserNotification() { }
        public int UserId { get; set; }
        public required User User { get; set; } 
        public int NotificationId { get; set; }
        public required Notification Notification { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
