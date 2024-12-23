using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Achievement
    {
        public Achievement() { }
        public Achievement(string text) { Text = text; }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Text { get; set; }
        public ICollection<UserAchievement>? UserAchievements { get; set; }
    }
}
