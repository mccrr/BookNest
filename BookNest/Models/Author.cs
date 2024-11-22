using System.ComponentModel.DataAnnotations;

namespace BookNest.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
