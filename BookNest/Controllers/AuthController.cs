using BookNest.Dtos.Users;
using BookNest.Models.Entities;
using BookNest.Services;
using BookNest.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace BookNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;
        public AuthController(UserService userService, TokenService tokenService) {
            _userService = userService;
            _tokenService = tokenService;
        }
        [HttpPost]
        [Route("signup")]
        public async Task<IBaseResponse> SignUp(SignUpDto signUpDto)
        {
            var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            if (!emailRegex.IsMatch(signUpDto.Email))
                return BaseResponse<object>.ErrorResponse(HttpStatusCode.BadRequest,"Invalid Email Address Format.");
            var dbUser = await _userService.CreateUser(signUpDto);
            if (dbUser == null) {
                return BaseResponse<object>.ErrorResponse(HttpStatusCode.BadRequest, "User couldnt be created");
            }
            return BaseResponse<UserDto>.SuccessResponse(new UserDto(dbUser));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IBaseResponse> LogIn(LoginDto logindto)
        {
            if (string.IsNullOrEmpty(logindto.Username) && string.IsNullOrEmpty(logindto.Email))
                return BaseResponse<object>.ErrorResponse(HttpStatusCode.BadRequest, "Username or email must be provided");

            User dbUser = null;

            if (!string.IsNullOrEmpty(logindto.Email))
                dbUser = await _userService.GetByEmail(logindto.Email);
            else if (!string.IsNullOrEmpty(logindto.Username))
                dbUser = await _userService.GetByUsername(logindto.Username);

            if (dbUser == null)
                return BaseResponse<object>.ErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");

            if (!_userService.VerifyPassword(logindto.Password, dbUser.Password))
                return BaseResponse<object>.ErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                new Claim(ClaimTypes.Email, dbUser.Email),
                new Claim("Username", dbUser.Username)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);

            var existingRfToken = await _tokenService.GetRefreshTokenByUser(dbUser.Id);
            if (existingRfToken != null) await _tokenService.RevokeRefreshTokenAsync(existingRfToken.Token);

            var refreshToken = await _tokenService.GenerateRefreshTokenAsync(dbUser.Id);
            var response = new AuthResponseDto(accessToken, refreshToken.Token, new UserDto(dbUser));

            return BaseResponse<AuthResponseDto>.SuccessResponse(response);
        }

        [HttpPost("logout")]
        public async Task<IBaseResponse> Logout()
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var refreshtoken = await _tokenService.GetRefreshTokenByUser(userId);
            if (refreshtoken == null) return BaseResponse<object>.ErrorResponse(HttpStatusCode.BadRequest, "You're already logged  out!");
            await _tokenService.RevokeRefreshTokenAsync(refreshtoken.Token);
            return BaseResponse<object>.SuccessResponse(null);
        }

        [HttpPost("refreshtoken")]
        public async Task<IBaseResponse> refreshToken([FromBody] string refreshtoken)
        {
            try
            {
                var dbToken = await _tokenService.ValidateRefreshTokenAsync(refreshtoken);
                if (dbToken.Token != refreshtoken)
                    return BaseResponse<object>.ErrorResponse(HttpStatusCode.BadRequest, "Token is invalid or expired");
                var dbUser = await _userService.GetById(dbToken.UserId);
                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                new Claim(ClaimTypes.Email, dbUser.Email),
                new Claim("Username", dbUser.Username)
            };
                var accessToken = _tokenService.GenerateAccessToken(claims);
                return BaseResponse<string>.SuccessResponse(accessToken);
            } catch (Exception E) { return BaseResponse<object>.ErrorResponse(HttpStatusCode.InternalServerError, E.Message); }
        }
    }
}
