using BookNest.Dtos.Users;
using BookNest.Models.Entities;
using BookNest.Services;
using BookNest.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace BookNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly FriendsService _friendsService;
        private readonly BookProgressService _bookProgressService;
        public UsersController(UserService userService, FriendsService friendsService = null, BookProgressService bookProgressService = null)
        {
            _userService = userService;
            _friendsService = friendsService;
            _bookProgressService = bookProgressService;
        }

        [HttpGet]
        public async Task<IBaseResponse> GetUsers(CancellationToken cancellationToken)
        {
                var result = await _userService.GetUsers(cancellationToken);
                return BaseResponse<List<User>>.SuccessResponse(result);
        }
        [HttpGet("id/{friendId}")]
        public async Task<IBaseResponse> GetUserById(int friendId)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _userService.GetById(friendId);
            var friendResult = await _friendsService.GetFriend(userId, friendId,false);
            var isFriend = (friendResult != null);
            var pendingSentRequest = false;
            var pendingReceivedRequest = false;
            if (!isFriend)
            {
                var sentRequest = await _friendsService.GetRequestByKey(userId,friendId,false);
                if (sentRequest != null)
                    pendingSentRequest = true;
                else
                {
                    var receivedRequest = await _friendsService.GetRequestByKey(friendId, userId,false);
                    if(receivedRequest != null) pendingReceivedRequest = true;
                }
            }
            var bookProgressResponse = await _bookProgressService.GetMyBooks(friendId);
            return BaseResponse<object>.SuccessResponse(
                new UserDto(result,pendingSentRequest,pendingReceivedRequest,isFriend,bookProgressResponse));
        }


        [HttpGet("profile")]
        public async Task<IBaseResponse> GetProfile()
        {
            if (HttpContext.User?.Claims == null || !HttpContext.User.Claims.Any())
            { return BaseResponse<object>.ErrorResponse(HttpStatusCode.Unauthorized, "You are not logged in!"); }
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                return BaseResponse<object>.ErrorResponse(HttpStatusCode.Unauthorized, "Claims were not found");
            int.TryParse(claim.Value, out int userId);
                var result = await _userService.GetById(userId);
                if (result == null) return BaseResponse<object>.ErrorResponse(HttpStatusCode.NotFound, "User Not Found");
                return BaseResponse<object>.SuccessResponse(new ProfileDto(result));
        }

        [HttpPut]
        public async Task<IBaseResponse> Update(UpdateUserDto dto)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var updatedUser = await _userService.UpdateUser(dto, userId);
            return BaseResponse<ProfileDto>.SuccessResponse(new ProfileDto(updatedUser));
        }

        [HttpDelete("{id}")]
        public async Task<IBaseResponse> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return BaseResponse<object>.SuccessResponse(null);
        }

    }
}
