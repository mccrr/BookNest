using BookNest.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos.Books
{
    public class BookDto
    {
        public BookDto(Book book, string author, double rating)
        {
            Isbn = book.Isbn;
            Title = book.Title;
            Author = author;
            Description = book.Description;
            Pages = book.Pages;
            Cover = book.Cover;
            Rating = rating;
        }

        [Required]
        [MaxLength(13)]
        public string Isbn { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        public double Rating { get; set; }

        [Required]
        public int Pages { get; set; }
        [Required]
        [MaxLength(2083)]
        public string Cover { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
    }
}
