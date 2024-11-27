using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Group
    {
        public Group() { }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<GroupRequest> GroupRequests { get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
    }
}
