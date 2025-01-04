using BookNest.Dtos.Review;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Review
    {
        public Review() { }

        public Review(ReviewDto reviewDto, int userId) {
            Text = reviewDto.Text;
            Rating = reviewDto.Rating;
            UserId = userId;
            BookId = reviewDto.BookId;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(1000)]
        public  string Text { get; set; }
        [Required]
        [Range(0, 10)]
        public double Rating { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public string BookId { get; set; }
        public Book Book { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
