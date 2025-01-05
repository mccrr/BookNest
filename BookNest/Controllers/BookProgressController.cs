using BookNest.Dtos.BookUsers;
using BookNest.Models.Entities;
using BookNest.Services;
using BookNest.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("api/bookprogress")]
    public class BookProgressController : ControllerBase
    {
        private readonly BookProgressService _bookProgressService;
        public BookProgressController(BookProgressService bookProgressService) {
            _bookProgressService = bookProgressService;
        }

        [HttpPost]
        public async Task<IBaseResponse> AddProgress(BookUserDto bookUserDto)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var bookProgress = await _bookProgressService.Create(bookUserDto, userId);
            return BaseResponse<BookUserDto>.SuccessResponse(bookProgress);
        }

        [HttpGet]
        public async Task<IBaseResponse> GetMyBooks()
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var myBooks = await _bookProgressService.GetMyBooks(userId);
            return BaseResponse<MyBooksResponseDto>.SuccessResponse(myBooks);
        }

        [HttpDelete("{isbn}")]
        public async Task<IBaseResponse> RemoveMyBook(string isbn)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _bookProgressService.RemoveMyBook(userId, isbn);
            return BaseResponse<object>.SuccessResponse(null);
        }

        //[HttpGet("max")]
        //public async Task<IBaseResponse> GetMax()
        //{
        //    var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    var progressList = await _bookProgressService.GetMax(userId);
        //    return BaseResponse<List<BookUser>>.SuccessResponse(progressList);
        //}

    }
}
