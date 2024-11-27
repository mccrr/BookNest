namespace BookNest.Models.Entities
{
    public class UserAchievement
    {
        public UserAchievement() { }
        public int UserId { get; set; }
        public required User User { get; set; }
        public int AchievementId { get; set; }
        public required Achievement Achievement { get; set; }

        public DateTime CreatedAt {  get; set; }= DateTime.Now;
    }
}
