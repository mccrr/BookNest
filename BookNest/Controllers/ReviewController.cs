using BookNest.Dtos;
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
        public ReviewController(ReviewService review)
        {
            _reviewService = review;
        }

        [HttpPost]
        public async Task<IBaseResponse> Create(ReviewDto reviewDto)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var review = await _reviewService.Create(reviewDto, userId);
            return BaseResponse<Review>.SuccessResponse(review);
        }

        [HttpGet("id/{id}")]
        public async Task<IBaseResponse> GetById(int id)
        {
            var review = await _reviewService.GetById(id);
            return BaseResponse<Review>.SuccessResponse(review);
        }

        [HttpGet("user/{id}")]
        public async Task<IBaseResponse> GetByUser(int id)
        {
            var reviews = await _reviewService.GetByUser(id);
            return BaseResponse<List<Review>>.SuccessResponse(reviews);
        }

        [HttpDelete("id/{id}")]
        public async Task<IBaseResponse> DeleteById(int id)
        {
            await _reviewService.DeleteById(id);
            return BaseResponse<object>.SuccessResponse("Review deleted successfully");
        }
    }
}
