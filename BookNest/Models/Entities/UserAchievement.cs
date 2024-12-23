namespace BookNest.Models.Entities
{
    public class UserAchievement
    {
        public UserAchievement() { }
        public UserAchievement(int id, int userId) 
        { 
            UserId = userId;
            AchievementId = id;
        }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AchievementId { get; set; }
        public Achievement Achievement { get; set; }

        public DateTime CreatedAt {  get; set; }= DateTime.Now;
    }
}
