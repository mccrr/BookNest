using BookNest.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos.Challenge
{
    public class ChallengeDto
    {
        public required string Text { get; set; }
        [Required]
        public required string Type { get; set; }
        [Required]
        public required int Objective { get; set; }
        [Required]
        public required bool IsCompleted { get; set; }
        [Required]
        public DateTime StartedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime EndsAt { get; set; }
    }
}
