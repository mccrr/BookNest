using BookNest.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos.BookUsers
{
    public class BookUserDto
    {
        public BookUserDto() { }
        public BookUserDto(BookUser bu)
        {
            BookId = bu.BookId;
            Status = bu.Status;
            Progress = bu.Progress;
        }
        public string BookId { get; set; }
        public string Status { get; set; }
        public int Progress { get; set; }
    }
}
