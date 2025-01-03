using BookNest.Dtos.Books;
using BookNest.Models.Entities;
using BookNest.Services;
using BookNest.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return BaseResponse<List<BookDto>>.SuccessResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IBaseResponse> GetBookById(string id)
        {
            var book = await _bookService.GetById(id);
            return BaseResponse<Book>.SuccessResponse(book);
        }
        [HttpPost]
        public async Task<IBaseResponse> AddBook(AddBookDto bookDto)
        {
            var book = new Book(bookDto);
            var dbBook = await _bookService.CreateBook(book);
            if (dbBook == null) 
                return BaseResponse<object>.ErrorResponse(System.Net.HttpStatusCode.BadRequest, "Book couldnt be created.");
            return BaseResponse<Book>.SuccessResponse(dbBook);
        }

        [HttpPost("{isbn}")]
        public async Task<IBaseResponse> UpdateBook(string isbn, UpdateBookDto bookDto)
        {
            var book =  await _bookService.Update(isbn, bookDto);
            return BaseResponse<Book>.SuccessResponse(book);
        }

        [HttpDelete("{isbn}")]
        public async Task<IBaseResponse> DeleteBook(string isbn)
        {
            await _bookService.Delete(isbn);
            return BaseResponse<object>.SuccessResponse(null);
        }

        [HttpPost("author")]
        public async Task<IBaseResponse> AddAuthor([FromBody] string name)
        {
            var author = await _bookService.AddAuthor(name);
            return BaseResponse<Author>.SuccessResponse(author);
        }
    }
}
