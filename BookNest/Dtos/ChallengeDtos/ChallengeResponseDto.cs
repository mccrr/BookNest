using System.ComponentModel.DataAnnotations;
using BookNest.Models.Entities;

namespace BookNest.Dtos.ChallengeDtos
{
    public class ChallengeResponseDto
    {
        public ChallengeResponseDto(Challenge challenge, int progress) { 
            Text = challenge.Text;
            Progress = progress;
            Type = challenge.Type;
            Objective = challenge.Objective;
            if (challenge.Objective <= progress) 
                IsCompleted = true;
            else 
                IsCompleted = false;
            StartedAt = challenge.StartedAt;
            EndsAt = challenge.EndsAt;
        }
        public string Text { get; set; }
        public string Type { get; set; }
        public int Objective { get; set; }
        public int Progress {  get; set; }
        public bool IsCompleted { get; set; }
        [Required]
        public DateTime StartedAt { get; set; } = DateTime.Now;
        public DateTime EndsAt { get; set; }
    }
}
