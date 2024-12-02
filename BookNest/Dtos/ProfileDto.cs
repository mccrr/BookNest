using BookNest.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookNest.Dtos
{
    public class ProfileDto
    {
        public ProfileDto(User user)
        {
            Id = user.Id;
            Username = user.Username;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Age = user.Age;
            Email = user.Email;
            Avatar = user.Avatar;
            BookUsers = user.BookUsers;
            UserAchievements = user.UserAchievements;
            UserNotifications = user.UserNotifications;
            SentFriendRequests = user.SentFriendRequests;
            ReceivedFriendRequests = user.ReceivedFriendRequests;
            GroupRequests = user.GroupRequests;
            GroupUsers = user.GroupUsers;
            FriendsInitiated = user.FriendsInitiated;
            FriendsReceived = user.FriendsReceived;
            Muted = user.Muted;
            Muter = user.Muter;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }

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
