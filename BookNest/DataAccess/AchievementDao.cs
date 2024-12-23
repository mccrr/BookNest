using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookNest.DataAccess
{
    public class AchievementDao
    {
        private readonly ApplicationDbContext _context;
        public AchievementDao(ApplicationDbContext context) { _context = context; }

        public async Task<Achievement> Create(Achievement ach)
        {
            var dbAch = await _context.Achievements.AddAsync(ach);
            await _context.SaveChangesAsync();
            return dbAch.Entity;
        }

        public async Task<List<Achievement>> GetAllAchievements()
        {
            return _context.Achievements.ToList();
        }
        public async Task<List<UserAchievement>> GetUserAch(int userId)
        {
            return _context.UserAchievements
                .Where(ua => ua.UserId==userId)
                .ToList();
        }

        public async Task<Achievement> GetById(int id)
        {
            return await _context.Achievements.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<UserAchievement> AssignToUser(UserAchievement userAch)
        {
            var result = await _context.UserAchievements.AddAsync(userAch);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
