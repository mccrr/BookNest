using BookNest.Dtos.Reviews;
using BookNest.Models.Entities;
using BookNest.Services;
using BookNest.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;
        private readonly UserService _userService;
        public ReviewController(ReviewService review, UserService userService)
        {
            _reviewService = review;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IBaseResponse> Create(ReviewDto reviewDto)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userService.GetById(userId);
            var review = await _reviewService.Create(reviewDto, userId);

            return BaseResponse<ReviewResponseDto>.SuccessResponse(new ReviewResponseDto(review, user));
        }

        [HttpGet("id/{id}")]
        public async Task<IBaseResponse> GetById(int id)
        {
            var review = await _reviewService.GetById(id);
            var user = await _userService.GetById(id);
            return BaseResponse<ReviewResponseDto>.SuccessResponse(new ReviewResponseDto(review, user));
        }

        [HttpGet("user/{id}")]
        public async Task<IBaseResponse> GetByUser(int id)
        {
            var reviews = await _reviewService.GetByUser(id);
            var result = new List<ReviewResponseDto>();
            foreach(Review review in reviews)
            {
                var user = await _userService.GetById(review.UserId);
                var dto = new ReviewResponseDto(review, user);
                result.Add(dto);
            }
            return BaseResponse<List<ReviewResponseDto>>.SuccessResponse(result);
        }

        [HttpGet("book/{isbn}")]
        public async Task<IBaseResponse> GetByBook(string isbn)
        {
            var reviews = await _reviewService.GetByBook(isbn);
            var result = new List<ReviewResponseDto>();
            foreach (Review review in reviews)
            {
                var user = await _userService.GetById(review.UserId);
                var dto = new ReviewResponseDto(review, user);
                result.Add(dto);
            }
            return BaseResponse<List<ReviewResponseDto>>.SuccessResponse(result);
        }

        [HttpDelete("id/{id}")]
        public async Task<IBaseResponse> DeleteById(int id)
        {
            await _reviewService.DeleteById(id);
            return BaseResponse<object>.SuccessResponse("Review deleted successfully");
        }
    }
}
