using BookNest.DataAccess;
using BookNest.Dtos.Achievement;
using BookNest.Models.Entities;
using BookNest.Utils;

namespace BookNest.Services
{
    public class AchievementService
    {
        private readonly AchievementDao _achievementDao;
        public AchievementService(AchievementDao achievementDao)
        {
            _achievementDao = achievementDao;
        }

        public async Task<List<Achievement>> GetAllAchievements()
        {
            return await _achievementDao.GetAllAchievements();
        }

        public async Task<Achievement> GetById(int id)
        {
            var achievement = await _achievementDao.GetById(id);
            if (achievement == null) throw new NotFoundException($"Achievement with id:{id} doesnt exist.");
            return achievement;
        }

        public async Task<List<UserAchievement>> GetUserAch(int userId)
        {
            return await _achievementDao.GetUserAch(userId);
        }

        public async Task<Achievement> Create(AchievementDto dto)
        {
            var achievement = new Achievement(dto.Text);
            var dbAchievement = await _achievementDao.Create(achievement);
            if (dbAchievement == null) throw new CustomException("Achievement couldnt be created");
            return dbAchievement;
        }
        
        public async Task<UserAchievement> AssignToUser(int id, int userId)
        {
            var achievement = await GetById(id);
            var existingUserAch = await GetUserAch(userId);
            if (existingUserAch != null) throw new CustomException("Achievement has already been assigned to this user.");
            var userAchievement = new UserAchievement(id, userId);
            var dbUa = await _achievementDao.AssignToUser(userAchievement);
            if (dbUa == null) throw new CustomException("Couldnt assign achievement to user");
            return dbUa;
        }
    }
}
