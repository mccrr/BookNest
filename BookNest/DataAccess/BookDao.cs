using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.EntityFrameworkCore;
using BookNest.Utils;

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

        public async Task<Book> GetByIdAsync(string isbn)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Isbn==isbn);
        }

        public async Task<Book> AddAsync(Book book)
        {
            var dbResult = await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return dbResult.Entity;
        }

        public async void UpdateAsync()
        {
            _context.SaveChanges();
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Author> CreateAuthor(Author author)
        {
            var result = await _context.Authors.AddAsync(author);
            _context.SaveChanges();
            return result.Entity;
        }

        
    }
}
