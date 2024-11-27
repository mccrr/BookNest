using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class GroupBook
    {
        public GroupBook() { }
        public string BookId { get; set; }
        public int UserId { get; set; } // Part of the composite key with GroupId
        public int GroupId { get; set; } // Part of the composite key with UserId

        public Book Book { get; set; } // Navigation property
        public GroupUser GroupUser { get; set; } // Navigation property

        [Required]
        [Range(0, 100)]
        public required int progress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
