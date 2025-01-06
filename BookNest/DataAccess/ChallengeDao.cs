using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookNest.DataAccess
{
    public class ChallengeDao
    {
        private readonly ApplicationDbContext _context;
        public ChallengeDao(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Challenge> AddAsync(Challenge challenge)
        {
            var result = await _context.Challenges.AddAsync(challenge);
            await _context.SaveChangesAsync();
            return result.Entity;
        } 

        public async Task<Challenge> GetById(int id)
        {
            return await _context.Challenges.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Challenge>> GetByUser(int userId)
        {
            return await _context.Challenges
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<Challenge> Update(int id, bool isCompleted)
        {
            var challenge = await _context.Challenges.FirstOrDefaultAsync(c => c.Id.Equals(id));
            challenge.IsCompleted= isCompleted;
            await _context.SaveChangesAsync();
            return challenge;
        }

    }
}
