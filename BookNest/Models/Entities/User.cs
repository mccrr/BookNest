using BookNest.Dtos.Users;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class User
    {
        public User() { }
        public User(SignUpDto signUpDto)
        {
            Username = signUpDto.Username;
            FirstName = signUpDto.FirstName;
            LastName = signUpDto.LastName;
            Age = signUpDto.Age;
            Email = signUpDto.Email;
            Password = signUpDto.Password;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(63)]
        public string Password { get; set; }
        [StringLength(255)]
        [DefaultValue("https://www.shutterstock.com/shutterstock/photos/1114445501/display_1500/stock-vector-blank-avatar-photo-place-holder-1114445501.jpg")]
        public string Avatar { get; set; } = "https://www.shutterstock.com/shutterstock/photos/1114445501/display_1500/stock-vector-blank-avatar-photo-place-holder-1114445501.jpg";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<BookUser>? BookUsers { get; set; }
        public ICollection<UserAchievement> UserAchievements { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }
        public ICollection<FriendRequest> SentFriendRequests { get; set; }
        public ICollection<FriendRequest> ReceivedFriendRequests { get; set; }
        public ICollection<GroupRequest> GroupRequests { get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
        public ICollection<Friend> FriendsInitiated { get; set; }
        public ICollection<Friend> FriendsReceived { get; set; }
        public ICollection<Mute> Muted { get; set; }
        public ICollection<Mute> Muter { get; set; }

    }
}
