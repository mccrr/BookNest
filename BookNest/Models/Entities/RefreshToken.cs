using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime ExpiresAt { get; set; } = DateTime.Now.AddDays(7);
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public User User { get; set; }
    }

}
