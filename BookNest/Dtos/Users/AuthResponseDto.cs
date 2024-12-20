using BookNest.Models.Entities;

namespace BookNest.Dtos.Users
{
    public class AuthResponseDto
    {
        public AuthResponseDto(string accessToken, string refreshToken, int user)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            UserId = user;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
