using System.ComponentModel.DataAnnotations;

namespace BookNest.Models
{
    public class Book
    {
        [Key]
        [Required]
        [MaxLength(13)]
        public required string Isbn { get; set; }
        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }

        [Required]
        public required int Pages { get; set; }
        [Required]
        [MaxLength(2083)]
        public required string Cover { get; set; }
        [Required]
        [MaxLength(1000)]
        public required string Description { get; set; }
        [Required]
        public required int AuthorId { get; set; }
        public required Author Author { get; set; }
        public ICollection<BookUser>? BookUsers { get; set; }
    }
}
