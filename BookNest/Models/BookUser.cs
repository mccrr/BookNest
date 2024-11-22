using System.ComponentModel.DataAnnotations;

namespace BookNest.Models
{
    public class BookUser
    {
        public int BookId { get; set; }
        public required Book Book { get; set; }

        public int UserId { get; set; }
        public required User User { get; set; }

        [Required]
        [MaxLength(13)]
        public required string status { get; set; }

        [Required]
        [Range(0, 100)]
        public required int progress { get; set; }
    }
}
