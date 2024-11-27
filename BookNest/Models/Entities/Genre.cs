using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Genre
    {
        public Genre() { }
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Text { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
