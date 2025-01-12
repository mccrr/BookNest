using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Services
{
    public class ForbiddenService
    {
        private readonly ApplicationDbContext _context;

        public ForbiddenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookUser> FindBookProgress(int userId, string isbn)
        {
            return await _context.BookUsers
                .Where(bu => bu.UserId == userId && bu.BookId == isbn)
                .OrderByDescending(x => x.Progress)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Book> GetBookById(string isbn)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Isbn == isbn);
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task RemoveNotifications(int userId, string BookId)
        {
            var notifications = await _context.Notifications
                .Where(x => x.UserId==userId && x.BookId==BookId)
                .ToListAsync();
            var userNotification = new UserNotification();
            foreach (var notification in notifications) { 
                _context.Notifications.Remove(notification);
                userNotification = await _context.UserNotifications.FirstOrDefaultAsync(x=>x.NotificationId==notification.Id);
                if (userNotification != null)
                {
                    _context.UserNotifications.Remove(userNotification);
                }
            }
            await _context.SaveChangesAsync();
        }

    }
}
