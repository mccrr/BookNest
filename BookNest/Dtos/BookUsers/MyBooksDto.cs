using BookNest.Models.Entities;

namespace BookNest.Dtos.BookUsers
{
    public class MyBooksDto
    {
        public MyBooksDto(BookUser bu, string cover, string title)
        {
            BookId = bu.BookId;
            Status = bu.Status;
            Progress = bu.Progress;
            Cover = cover;
            Title = title;
        }
        public string BookId { get; set; }
        public string Status { get; set; }
        public int Progress { get; set; }
        public string Cover { get; set; }
        public string Title { get; set; }
    }
}
