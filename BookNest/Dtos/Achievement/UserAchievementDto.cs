using BookNest.Models.Entities;

namespace BookNest.Dtos.Achievement
{
    public class UserAchievementDto
    {
        public UserAchievementDto(int user, int ach) 
        {
            AchievementId = ach;
            UserId = user;
        }
        public UserAchievementDto(UserAchievement a)
        {
            AchievementId = a.AchievementId;
            UserId = a.UserId;
        }
        public int AchievementId { get; set; }
        public int UserId { get; set; }
    }
}
