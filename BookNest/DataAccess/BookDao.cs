using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookNest.DataAccess
{
    public class BookDao
    {
        private readonly ApplicationDbContext _context;
        public BookDao(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<List<Book>> GetBooksAsync(CancellationToken cancellationToken)
        {
            return await _context.Books.ToListAsync(cancellationToken);
        }
    }
}
