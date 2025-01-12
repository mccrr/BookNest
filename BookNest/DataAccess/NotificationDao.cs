using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookNest.DataAccess
{
    public class NotificationDao
    {
        private readonly ApplicationDbContext _context;
        public NotificationDao(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Notification> AddAsync(Notification notification)
        {
            var result = await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Notification> GetById(int id)
        {
            return await _context.Notifications.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task Delete(Notification notification)
        {
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserNotification>> GetAllUserNotifications(int userId)
        {
            return await _context.UserNotifications
                .Where(u => u.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<UserNotification> CreateUserNotification(int userId, int notifId)
        {
            var userNoti = new UserNotification(userId, notifId);
            var dbUn = await _context.UserNotifications.AddAsync(userNoti);
            await _context.SaveChangesAsync();
            return dbUn.Entity;
        }

        ////////////////FORBIDDEN CODE///////////////////////////////
    }
}
