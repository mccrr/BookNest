using BookNest.DataAccess;
using BookNest.Dtos.Notifications;
using BookNest.Models.Entities;
using BookNest.Utils;

namespace BookNest.Services
{
    public class NotificationService
    {
        private readonly NotificationDao _notificationDao;
        public NotificationService(NotificationDao notificationDao)
        {
            _notificationDao = notificationDao;
        }

        public async Task<Notification> Create(NotificationDto dto)
        {
            var notification = new Notification(dto);
            var dbNotification = await _notificationDao.AddAsync(notification);
            if (dbNotification == null) throw new CustomException("Notification couldnt be created!");
            return dbNotification;
        }

        public async Task<Notification> GetById(int id)
        {
            var notification = await _notificationDao.GetById(id);
            if (notification == null) throw new NotFoundException($"Notification with id:{id} not found!");
            return notification;
        }

        public async Task Delete(int id)
        {
            var notification = await GetById(id);
            await _notificationDao.Delete(notification);
        }
    }
}
