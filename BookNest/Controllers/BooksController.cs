using BookNest.Models.Entities;
using BookNest.Services;
using BookNest.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class BooksController
    {
        private readonly BookService _bookService;
        public BooksController(BookService bookService) { 
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<IBaseResponse> GetBooks(CancellationToken cancellationToken)
        {
            var result = await _bookService.GetBooks(cancellationToken);
            return BaseResponse<List<Book>>.SuccessResponse(result);
        }
    }
}
