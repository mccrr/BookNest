using BookNest.DataAccess;
using BookNest.Dtos.Challenge;
using BookNest.Models.Entities;
using BookNest.Utils;

namespace BookNest.Services
{
    public class ChallengeService
    {
        private readonly ChallengeDao _challengeDao;
        public ChallengeService(ChallengeDao challengeDao)
        {
            _challengeDao = challengeDao;
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

        public async Task<Challenge> Update(int id, int userId)
        {
            var challenge = await GetById(id, userId);
            var dbChallenge = await _challengeDao.Update(challenge.Id);
            return challenge;
        }
    }
}
