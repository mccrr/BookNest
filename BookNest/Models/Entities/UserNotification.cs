namespace BookNest.Models.Entities
{
    public class UserNotification
    {
        public UserNotification() { }
        public UserNotification(int userId, int notId) { 
            UserId = userId;
            NotificationId = notId;
        }
        public int UserId { get; set; }
        public  User User { get; set; } 
        public int NotificationId { get; set; }
        public  Notification Notification { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
