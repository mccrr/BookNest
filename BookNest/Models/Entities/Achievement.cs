using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Achievement
    {
        public Achievement() { }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Text { get; set; }
        public ICollection<UserAchievement>? UserAchievements { get; set; }
    }
}
