using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookNest.DataAccess
{
    public class ShelfBookDao
    {
        private readonly ApplicationDbContext _context;
        public ShelfBookDao(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<ShelfBook> AddAsync(ShelfBook shelfBook)
        {
            var result = await _context.ShelfBooks.AddAsync(shelfBook);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ShelfBook> FindByKey(string isbn, int shelfId) 
        { 
            return await _context.ShelfBooks.FirstOrDefaultAsync(s => s.ShelfId == shelfId && s.BookId == isbn );
        }

        public async Task Remove(ShelfBook shelfBook)
        {
            _context.ShelfBooks.Remove(shelfBook);
            await _context.SaveChangesAsync();
        }
    }
}
