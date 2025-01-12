using BookNest.Models.Entities;

namespace BookNest.Dtos.Friends
{
    public class FriendRequestResponseDto
    {
        public FriendRequestResponseDto(FriendRequest fr, string username, string avatar)
        {
            SenderId = fr.SenderId;
            ReceiverId = fr.ReceiverId;
            CreatedAt = fr.CreatedAt;
            Username = username;
            Avatar = avatar;
        }
        public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Username { get; set; }
    public string Avatar{ get; set; }
    }
}
