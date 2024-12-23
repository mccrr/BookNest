using BookNest.Dtos.Achievement;
using BookNest.Models.Entities;
using BookNest.Services;
using BookNest.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("/api/achievements")]
    public class AchievementController : ControllerBase
    {
        private readonly AchievementService _achievementService;
        public AchievementController(AchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        [HttpGet]
        public async Task<IBaseResponse> GetAllAchievements()
        {
            var achievements = await _achievementService.GetAllAchievements();
            return BaseResponse<List<Achievement>>.SuccessResponse(achievements); 
        }

        [HttpGet("user")]
        public async Task<IBaseResponse> GetUserAchievements()
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var achievements = await _achievementService.GetUserAch(userId);
            return BaseResponse<List<UserAchievementDto>>
                .SuccessResponse(achievements.Select(x=>new UserAchievementDto(x)).ToList());
        }

        [HttpGet("{id}")]
        public async Task<IBaseResponse> GetById(int id)
        {
            var achievement = await _achievementService.GetById(id);
            return BaseResponse<Achievement>.SuccessResponse(achievement);
        }

        [HttpPost]
        public async Task<IBaseResponse> Create(AchievementDto dto)
        {
            var achievement = await _achievementService.Create(dto);
            return BaseResponse<Achievement>.SuccessResponse(achievement);
        }

        [HttpPost("{id}")]
        public async Task<IBaseResponse> AssignToUser(int id)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userAchievement = await _achievementService.AssignToUser(id, userId);
            return BaseResponse<UserAchievementDto>
                .SuccessResponse(new UserAchievementDto(userAchievement.AchievementId, userAchievement.UserId));
        }
    }
}
