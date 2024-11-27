using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class GroupRequest
    {
        public GroupRequest() { }
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
