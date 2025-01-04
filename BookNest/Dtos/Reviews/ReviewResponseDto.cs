using BookNest.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos.Reviews
{
    public class ReviewResponseDto
    {
        public ReviewResponseDto(Review review, User user)
        {
            Id = review.Id;
            Text = review.Text;
            Rating = review.Rating;
            UserName = user.Username;
            Avatar = user.Avatar;
            BookId = review.BookId;
            CreatedAt = review.CreatedAt;
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public float Rating { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }

        public string BookId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
