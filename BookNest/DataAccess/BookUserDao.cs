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
                .Where(bu => bu.UserId == userId && bu.BookId == isbn && bu.Progress == progress)
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

        
        public async Task<BookUser> GetBookStatus(int userId, string bookId, string status)
        {
            return await _context.BookUsers
                .FirstOrDefaultAsync(bu => bu.UserId == userId && bu.Status == status && bu.BookId == bookId);
        }

        public async Task<List<BookUser>> GetAllReading(int userId, string bookId)
        {
            return await _context.BookUsers
                .Where(bu => bu.UserId == userId && bu.Status == "reading" && bu.BookId == bookId)
                .ToListAsync();
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

        public async Task<List<BookUser>> GetAllByUserAndBook(int userId, string bookId)
        {
            var result = await _context.BookUsers
                .Where(bu => bu.UserId == userId && bu.BookId == bookId)
                .ToListAsync();
            if (result.Count == 0) Console.WriteLine("result is null");
            Console.WriteLine("result: ", result.Count);
            return result;
        }
        public async Task Delete(BookUser bookUser)
        {
            _context.Remove(bookUser);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetPagesBetweenDates(int userId, DateTime startingDate, DateTime endDate)
        {
            var pages = 0;
            var groupedByBook = await GetByUser(userId);
            int startingCheckpoint = 0;
            int endingCheckpoint = 0;
            foreach (var group in groupedByBook)
            {
                var existing = _context.BookUsers
                    .Where(x => x.UserId == userId && x.BookId == group.BookId && x.CreatedAt < startingDate)
                    .OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefault();
                var lastProgress = await _context.BookUsers
                .Where(bu => bu.UserId == userId && bu.BookId == group.BookId && bu.CreatedAt >= startingDate && bu.CreatedAt <= endDate)
               .OrderByDescending(x => x.Progress)
                .FirstOrDefaultAsync();
                Console.WriteLine($"Group: {group.BookId} {group.Progress} {group.Status} {group.CreatedAt}");
                Console.WriteLine($"startingDate: {startingDate} endDate: {endDate} group.CreatedAt: {group.CreatedAt}");
                //Console.WriteLine($"lastProgress: {lastProgress.BookId} {lastProgress.Progress} {lastProgress.Status} {lastProgress.CreatedAt}");
                if (lastProgress != null) 
                    endingCheckpoint=lastProgress.Progress;
                if (existing != null)
                    startingCheckpoint = existing.Progress;
                Console.WriteLine($"endingcheckpoint: {endingCheckpoint} - startingCheckpoint: {startingCheckpoint}");
                var book = await _context.Books.FirstOrDefaultAsync(x => x.Isbn == group.BookId);
                if (group.Progress == 0 || book.Pages==0 || endingCheckpoint==0) continue;
                pages += (int)(book.Pages * (endingCheckpoint - startingCheckpoint) / 100 );
                startingCheckpoint = 0;
                endingCheckpoint = 0;
            }
            Console.WriteLine($"Pages: {pages}");
            return pages;
        }

        public async Task<int> GetBooksBetweenDates(int userId, DateTime startingDate, DateTime endDate)
        {

            var read = await GetByStatus(userId, "read");
            return read.Where(x => x.CreatedAt >= startingDate && x.CreatedAt <= endDate).ToList().Count;
        }

    }
}
