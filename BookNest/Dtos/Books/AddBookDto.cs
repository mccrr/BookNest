using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos.Books
{
    public class AddBookDto
    {
        public required string Isbn { get; set; }
    }
}
