using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class GroupUser
    {
        public GroupUser() { }
        [Key]
        public int Id { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<GroupBook> GroupBooks { get; set; }
    }
}
