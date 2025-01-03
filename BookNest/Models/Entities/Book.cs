using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Book
    {
        public Book() { }

        public Book(string isbn, string title, int pages, string cover, string description, int authorId, string publisher, DateTime publishedDate, string language, string category)
        {
            Isbn = isbn;
            Title = title;
            Pages = pages;
            Cover = cover;
            Description = description;
            AuthorId = authorId;
            Publisher = publisher;
            PublishedDate = publishedDate;
            Language = language;
            Category = category;
        }

        [Key]
        [Required]
        [MaxLength(13)]
        public string Isbn { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public int Pages { get; set; }

        [Required]
        [MaxLength(2083)] // Max length for a URL
        public string Cover { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [MaxLength(200)]
        public string Publisher { get; set; }

        [MaxLength(10)]
        public DateTime PublishedDate { get; set; }

        [MaxLength(10)]
        public string Language { get; set; }

        [MaxLength(200)]
        public string Category { get; set; }

        public ICollection<BookUser>? BookUsers { get; set; }
        public ICollection<ShelfBook>? ShelfBooks { get; set; }
        public ICollection<GroupBook>? GroupBooks { get; set; }
    }
}
