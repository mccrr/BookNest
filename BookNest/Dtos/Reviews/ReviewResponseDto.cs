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
            Username = user.Username;
            Avatar = user.Avatar;
            BookId = review.BookId;
            CreatedAt = review.CreatedAt.ToString("o");
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public float Rating { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }

        public string BookId { get; set; }

        public string CreatedAt { get; set; }
    }
}
