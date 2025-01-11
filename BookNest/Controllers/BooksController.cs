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
        private readonly ReviewService _reviewService;
        private readonly BookService _bookService;
        private readonly GoogleService _googleService;
        public BooksController(BookService bookService, GoogleService googleService, ReviewService reviewService) { 
            _bookService = bookService;
            _googleService = googleService;
            _reviewService = reviewService;
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
            var book = await _bookService.GetById(id, true);
            var author = await _bookService.GetAuthorById(book.AuthorId);
            var rating = await _reviewService.GetRating(book.Isbn);
            var responseDto = new BookDto(book, author.Name, rating);
            return BaseResponse<BookDto>.SuccessResponse(responseDto);
        }
        [HttpPost]
        public async Task<IBaseResponse> AddBook(AddBookDto bookDto)
        {
            var existingBook = await _bookService.GetById(bookDto.Isbn, false);
            if (existingBook != null) return BaseResponse<object>.ErrorResponse(System.Net.HttpStatusCode.OK, "Book already exists");
            var book = await _googleService.GetBookInfoAsync(bookDto.Isbn);
            var dbBook = await _bookService.CreateBook(book);
            if (dbBook == null) 
                return BaseResponse<object>.ErrorResponse(System.Net.HttpStatusCode.BadRequest, "Book couldnt be created.");
            var author = await _bookService.GetAuthorById(book.AuthorId);
            var rating = await _reviewService.GetRating(book.Isbn);
            return BaseResponse<BookDto>.SuccessResponse(new BookDto(dbBook, author.Name, rating));
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

