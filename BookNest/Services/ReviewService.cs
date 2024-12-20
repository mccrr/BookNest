using BookNest.DataAccess;
using BookNest.Dtos;
using BookNest.Models.Entities;
using BookNest.Utils;

namespace BookNest.Services
{
    public class ReviewService
    {
        private readonly ReviewDao _reviewDao;
        private readonly UserService _userService;
        public ReviewService(ReviewDao reviewDao, UserService userService) { 
            _reviewDao = reviewDao;
            _userService = userService;
        }

        public async Task<Review> Create(ReviewDto reviewDto, int userId)
        {
            var review = new Review(reviewDto, userId);
            var dbReview = await _reviewDao.AddAsync(review);
            if (dbReview == null) throw new CustomException("Review couldnt be submitted!");
            return dbReview;
        }
        public async Task<List<Review>> GetByUser(int id)
        {
            var user = await _userService.GetById(id);
            var reviews = await _reviewDao.GetByUser(id);
            if (reviews == null) throw new CustomException("Internal Server Error!");
            return reviews;
        }

        public async Task<Review> GetById(int id)
        {
            var review = await _reviewDao.GetById(id);
            if (review == null) throw new NotFoundException($"Review with id: {id} doesnt exist.");
            return review;
        }


        public async Task DeleteById(int id)
        {
            var review = await GetById(id);
            await _reviewDao.Delete(review);
        }
    }
}
