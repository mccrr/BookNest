using BookNest.DataAccess;
using BookNest.Dtos.ChallengeDtos;
using BookNest.Dtos.ChallengeDtos;
using BookNest.Models.Entities;
using BookNest.Utils;

namespace BookNest.Services
{
    public class ChallengeService
    {
        private readonly ChallengeDao _challengeDao;
        private readonly BookUserDao _bookUserDao;
        public ChallengeService(ChallengeDao challengeDao, BookUserDao bookUserDao)
        {
            _challengeDao = challengeDao;
            _bookUserDao = bookUserDao;
        }

        public async Task<List<Challenge>> GetByUser(int userId)
        {
            return await _challengeDao.GetByUser(userId);
        }

        public async Task<Challenge> GetById(int id, int userId)
        {
            var challenge = await _challengeDao.GetById(id);

            if (challenge == null) throw new NotFoundException($"Challenge with id: {id} does not exist!");
            if (userId != challenge.UserId) throw new UnauthorizedException("Cannot access other users' challenges!");
            return challenge;
        }

        public async Task<Challenge> Create(ChallengeDto challengeDto, int userId)
        {
            if (challengeDto.Type.ToLower() != "start" && challengeDto.Type.ToLower() != "book"
                && challengeDto.Type.ToLower() != "pages")
                throw new CustomException("Challenge type is invalid");
            var challenge = new Challenge(challengeDto, userId);
            var dbChallenge = await _challengeDao.AddAsync(challenge);
            if (dbChallenge == null) throw new CustomException("Challenge couldnt be created!");
            return challenge;
        }

        public async Task<Challenge> Update(int id, int userId, bool completed)
        {
            var challenge = await GetById(id, userId);
            var dbChallenge = await _challengeDao.Update(id, completed);
            return dbChallenge;
        }

        public async Task<int> GetPagesBetweenDates(int userId,int challengeId, DateTime startDate, DateTime endDate)
        {
            var challenge = await _challengeDao.GetById(challengeId);
            var pages = await _bookUserDao.GetPagesBetweenDates(userId, challenge.StartedAt, challenge.EndsAt);
            return pages;
        }

        public async Task<int> GetBooksBetweenDates(int userId, int challengeId, DateTime startDate, DateTime endDate)
        {
            var challenge = await _challengeDao.GetById(challengeId);
            var books = await _bookUserDao.GetBooksBetweenDates(userId, challenge.StartedAt, challenge.EndsAt);
            return books;
        }
    }
}
