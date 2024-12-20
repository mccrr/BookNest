using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos.Books
{
    public class AddBookDto
    {
        public required string Isbn { get; set; }
        public required string Title { get; set; }
        public required int Pages { get; set; }
        public required string Cover { get; set; }
        public required string Description { get; set; }
        public required int AuthorId { get; set; }
    }
}
