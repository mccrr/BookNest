namespace BookNest.Models.Entities
{
    public class Friend
    {
        public Friend() { }
        public int UserId { get; set; }
        public required User User { get; set; }

        public int FriendId { get; set; }
        public required User Friendd { get; set; }
        public int? DominatorId { get; set; }
        public User? Dominator { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
