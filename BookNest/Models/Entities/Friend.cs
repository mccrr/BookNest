namespace BookNest.Models.Entities
{
    public class Friend
    {
        public Friend() { }
        public Friend(int userId, int friendId) { 
            UserId = userId;
            FriendId = friendId;
        }
        public int UserId { get; set; }
        public User User { get; set; }

        public int FriendId { get; set; }
        public User Friendd { get; set; }
        public int? DominatorId { get; set; } = null;
        public User? Dominator { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
