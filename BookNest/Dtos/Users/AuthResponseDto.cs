using BookNest.Models.Entities;

namespace BookNest.Dtos.Users
{
    public class AuthResponseDto
    {
        public AuthResponseDto(string accessToken, string refreshToken, UserDto user)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            User = user;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserDto User { get; set; }
    }
}
