using BookNest.Dtos.Challenge;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Challenge
    {
        public Challenge() { }

        public Challenge(ChallengeDto dto, int userId)
        {
            UserId = userId;
            Text = dto.Text;
            Objective = dto.Objective;
            Type = dto.Type;
            IsCompleted = dto.IsCompleted;
            StartedAt = dto.StartedAt;
            EndsAt = dto.EndsAt;
        }
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Objective { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime StartedAt { get; set; }
        [Required]
        public DateTime EndsAt { get; set; }
    }
}
