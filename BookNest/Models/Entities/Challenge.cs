using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Challenge
    {
        public Challenge() { }
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        [Required]
        public required string Text { get; set; }
        [Required]
        public required string Type { get; set; }
        [Required]
        public required int Objective { get; set; }
        [Required]
        public required bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime StartedAt { get; set; }
        [Required]
        public DateTime EndsAt { get; set; }
    }
}
