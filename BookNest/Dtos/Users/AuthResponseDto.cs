﻿using BookNest.Models.Entities;

namespace BookNest.Dtos.Users
{
    public class AuthResponseDto
    {
        public AuthResponseDto(string accessToken, string refreshToken, ProfileDto user)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            User = user;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public ProfileDto User { get; set; }
    }
}
