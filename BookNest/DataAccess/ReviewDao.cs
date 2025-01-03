using BookNest.Data;
using BookNest.Dtos;
using BookNest.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookNest.DataAccess
{
    public class ReviewDao
    {
        private readonly ApplicationDbContext _context;
        public ReviewDao(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Review> AddAsync(Review review)
        {
            var dbReview = await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return dbReview.Entity;
        }

        public async Task<List<Review>> GetByUser(int userId)
        {
            var result = _context.Reviews.Where(r => r.UserId == userId);
            return result.ToList();
        }

        public async Task<List<Review>> GetByBook(string isbn)
        {
            var result = _context.Reviews.Where(r => r.BookId == isbn);
            return result.ToList();
        }

        public async Task<Review> GetById(int id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Delete(Review review)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }


        public async Task<double> GetRating(string id)
        {
            var ratings = _context.Reviews.Where(b => b.BookId == id);

            if (!ratings.Any())
            {
                return 0.0;
            }
            return _context.Reviews
                .Where(b => b.BookId == id)
                .Average(r => r.Rating);
        }

    }
}
