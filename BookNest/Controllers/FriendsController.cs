using BookNest.Dtos.Friends;
using BookNest.Models.Entities;
using BookNest.Services;
using BookNest.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("/api/friends")]
    public class FriendsController : ControllerBase
    {
        private readonly FriendsService _friendsService;
        public FriendsController(FriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        [HttpPost]
        public async Task<IBaseResponse> SendRequest(FRequestDto dto)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId == dto.friendId)
                return BaseResponse<object>.ErrorResponse(System.Net.HttpStatusCode.BadRequest, "Cant send friend requests to yourself.");
            var friendRequest = await _friendsService.SendRequest(userId, dto.friendId);
            return BaseResponse<FriendRequestDto>.SuccessResponse(new FriendRequestDto(friendRequest));
        }


        [HttpPost("response")]
        public async Task<IBaseResponse> SendResponse(FRequestResponseDto dto)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var friend = await _friendsService.SendResponse(dto,userId);
            if (friend == null)
                return BaseResponse<object>.SuccessResponse(null);
            return BaseResponse<FriendDto>.SuccessResponse(new FriendDto(friend));
        }

        [HttpGet("requests/sent")]
        public async Task<IBaseResponse> GetSentRequestByUser()
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var requests = await _friendsService.GetSentRequestsByUser(userId);
            return BaseResponse<List<FriendRequestResponseDto>>.SuccessResponse(requests);
        }

        [HttpGet("requests/received")]
        public async Task<IBaseResponse> GetReceivedRequestByUser()
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var requests = await _friendsService.GetReceivedRequestsByUser(userId);
            return BaseResponse<List<FriendRequestResponseDto>>.SuccessResponse(requests);
        }

        [HttpDelete("requests/{friendId}")]
        public async Task<IBaseResponse> RemoveRequest(int friendId)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _friendsService.RemoveRequest(userId,friendId);
            return BaseResponse<object>.SuccessResponse(null);
        }

        [HttpDelete("{friendId}")]
        public async Task<IBaseResponse> RemoveFriend(int friendId)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _friendsService.RemoveFriend(userId,friendId);
            return BaseResponse<object>.SuccessResponse(null);
        }

        [HttpGet]
        public async Task<IBaseResponse> GetFriends()
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var friends = await _friendsService.GetAllFriends(userId);
            return BaseResponse<List<int>>.SuccessResponse(friends);
        }

    }
}
