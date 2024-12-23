using BookNest.Models.Entities;

namespace BookNest.Dtos.Friends
{
    public class FriendRequestDto
    {
        public FriendRequestDto(FriendRequest fr)
        {
            SenderId = fr.SenderId;
            ReceiverId = fr.ReceiverId;
            CreatedAt = fr.CreatedAt;
        }
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
