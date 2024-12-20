using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookNest.DataAccess
{
    public class ShelfDao
    {
        private readonly ApplicationDbContext _context;

        public ShelfDao(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Shelf> AddAsync(Shelf shelf)
        {
            var dbResult = await _context.Shelves.AddAsync(shelf);
            return dbResult.Entity;
        }

        public async Task<List<Shelf>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Shelves.ToListAsync(cancellationToken);
        }

        public async Task<Shelf> GetById(int id)
        {
            return await _context.Shelves.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
