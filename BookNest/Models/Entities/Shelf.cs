using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Shelf
    {
        public Shelf() { }
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public ICollection<ShelfBook>? ShelfBooks { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
