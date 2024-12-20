using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookNest.Dtos.Users
{
    public class UpdateUserDto
    {
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Avatar { get; set; }
    }
}
