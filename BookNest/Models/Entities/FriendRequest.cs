namespace BookNest.Models.Entities
{
    public class FriendRequest
    {
        public FriendRequest() { }
        public int SenderId { get; set; }
        public User Sender { get; set; }

        public int ReceiverId { get; set; }
        public User Receiver { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
