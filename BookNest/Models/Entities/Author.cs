using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Author
    {
        public Author() { }

        public Author(string name) {
            Name = name; 
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
