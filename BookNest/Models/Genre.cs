using System.ComponentModel.DataAnnotations;

namespace BookNest.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Text { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
