using BookNest.Data;
using BookNest.Models.Entities;

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
            return result.Entity;
        }
        public async Task Delete(BookUser bookUser)
        {
            _context.Remove(bookUser);
            _context.SaveChanges();
        }

    }
}
