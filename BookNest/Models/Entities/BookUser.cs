using BookNest.Dtos.BookUsers;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class BookUser
    {
        public BookUser(BookUserDto bookUserDto)
        {
            BookId = bookUserDto.BookId;
            UserId = bookUserDto.UserId;
            Progress = bookUserDto.Progress;
            Status = bookUserDto.Status;
        }
        public BookUser() { }
        public string BookId { get; set; }
        public Book Book { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [MaxLength(13)]
        public string Status { get; set; }

        [Required]
        [Range(0, 100)]
        public int Progress { get; set; }
    }
}
