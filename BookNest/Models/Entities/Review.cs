using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Review
    {
        public Review() { }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(1000)]
        public required string Text { get; set; }
        [Required]
        [Range(0, 10)]
        public required int rating { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public string BookId { get; set; }
        public Book Book { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
