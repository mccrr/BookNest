using BookNest.DataAccess;
using BookNest.Dtos.Books;
using BookNest.Models.Entities;
using BookNest.Utils;

namespace BookNest.Services
{
    public class BookService
    {
        private readonly BookDao _bookDao;
        public BookService(BookDao bookDao) {
            _bookDao = bookDao;        
        }

        public async Task<List<Book>> GetBooks(CancellationToken cancellationToken)
        {
            return await _bookDao.GetBooksAsync(cancellationToken);
        }

        public async Task<Book> GetById(string isbn)
        {
            var book = await _bookDao.GetByIdAsync(isbn);
            if(book == null) throw new NotFoundException($"Book with isbn: {isbn} doesnt exist in our database.");
            return book;
        }

        public async Task<Book> CreateBook(Book book)
        {
            var dbBook = await _bookDao.AddAsync(book);
            if (dbBook == null)
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, "Book couldnt be created");
            return dbBook;
        }

        public async Task<Book> Update(string isbn, UpdateBookDto bookDto)
        {
            var book = await GetById(isbn);
            if(bookDto.Title!=null) book.Title = bookDto.Title;
            //if (bookDto.Pages != null) book.Pages = bookDto.Pages;
            if (bookDto.Description != null) book.Description= bookDto.Description;
            //if (bookDto.AuthorId != null) book.AuthorId = bookDto.AuthorId;
            if (bookDto.Cover != null) book.Cover= bookDto.Cover;
            _bookDao.UpdateAsync();
            return book;
        }

        public async Task<Author> AddAuthor(string name)
        {
            var author = new Author(name);
            var dbAuthor = await _bookDao.CreateAuthor(author);
            return dbAuthor;
        }

        public async Task Delete(string isbn)
        {
            var book = await GetById(isbn);
            await _bookDao.DeleteAsync(book);
        }
    }
}
