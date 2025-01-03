using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookNest.DataAccess
{
    public class BookUserDao
    {
        private readonly ApplicationDbContext _context;
        public BookUserDao(ApplicationDbContext dbContext) {
            _context = dbContext;
        }

        public async Task<BookUser> Add(BookUser bookUser)
        {
            var result = await _context.BookUsers.AddAsync(bookUser);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<BookUser> FindByKey(int userId, string isbn, int progress)
        {
            return await _context.BookUsers
                .Where(bu => bu.UserId == userId && bu.BookId == isbn)
                .OrderByDescending(bu => bu.Progress)
                .FirstOrDefaultAsync();
        }

        public async Task<List<BookUser>> GetAllByUser(int userId)
        {
            var result = await _context.BookUsers
                .Where(bu => bu.UserId == userId)
                .OrderByDescending(bu => bu.Progress)
                .ToListAsync();
            return result;
        }

        public async Task<List<BookUser>> GetByStatus(int userId, string status)
        {
            return await _context.BookUsers
                .Where(bu => bu.UserId == userId && bu.Status == status)
                .GroupBy(bu => bu.BookId)
                .Select(group => group
                .OrderByDescending(bu => bu.Progress)
                .FirstOrDefault())
                .ToListAsync();
        }

        public async Task<List<BookUser>> GetByUser(int userId)
        {
            return await _context.BookUsers
                .Where(bu => bu.UserId == userId)
                .GroupBy(bu => bu.BookId)
                .Select(group => group
                .OrderByDescending(bu=>bu.Progress)
                .FirstOrDefault())
                .ToListAsync();
        }
        public async Task Delete(BookUser bookUser)
        {
            _context.Remove(bookUser);
            await _context.SaveChangesAsync();
        }

    }
}
