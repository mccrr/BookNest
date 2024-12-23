using BookNest.Models.Entities;

namespace BookNest.Dtos.Friends
{
    public class FriendDto
    {
        public FriendDto(Friend friend)
        {
            UserId = friend.UserId;
            FriendId = friend.FriendId;
            DominatorId = friend.DominatorId;
            CreatedAt = friend.CreatedAt;
        }
        public int UserId { get; set; }

        public int FriendId { get; set; }
        public int? DominatorId { get; set; } = null;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
