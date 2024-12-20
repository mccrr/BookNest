using BookNest.Dtos.Books;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Book
    {
        public Book() { }

        public Book(AddBookDto bookDto)
        {
            Isbn=bookDto.Isbn;
            Title=bookDto.Title;
            AuthorId=bookDto.AuthorId;
            Description=bookDto.Description;   
            Pages = bookDto.Pages;
            Cover= bookDto.Cover;
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
        [MaxLength(2083)]
        public string Cover { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<BookUser>? BookUsers { get; set; }
        public ICollection<ShelfBook>? ShelfBooks { get; set; }
        public ICollection<GroupBook>? GroupBooks { get; set; }
    }
}
