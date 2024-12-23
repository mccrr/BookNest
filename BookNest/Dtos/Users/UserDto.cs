using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BookNest.Models.Entities;

namespace BookNest.Dtos.Users
{
    public class UserDto
    {
        public UserDto(User user)
        {
            Id = user.Id;
            Username = user.Username;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Age = user.Age;
            Email = user.Email;
            Avatar = user.Avatar;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
    }
}
