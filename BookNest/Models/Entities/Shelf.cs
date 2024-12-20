using BookNest.Dtos.Shelf;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Shelf
    {
        public Shelf() { }

        public Shelf(CreateShelfDto shelfDto) { 
            Name = shelfDto.Name;
            UserId = shelfDto.UserId;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<ShelfBook>? ShelfBooks { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
