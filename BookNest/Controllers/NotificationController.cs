using BookNest.Dtos.Notifications;
using BookNest.Models.Entities;
using BookNest.Services;
using BookNest.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;
        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IBaseResponse> Create(NotificationDto dto)
        {
            var notification = await _notificationService.Create(dto);
            return BaseResponse<Notification>.SuccessResponse(notification);
        }

        [HttpGet("{id}")]
        public async Task<IBaseResponse> GetById(int id)
        {
            var notification = await _notificationService.GetById(id);
            return BaseResponse<Notification>.SuccessResponse(notification);
        }

        [HttpDelete("{id}")]
        public async Task<IBaseResponse> Delete(int id)
        {
            await _notificationService.Delete(id);
            return BaseResponse<object>.SuccessResponse(null);
        }
    }
}
