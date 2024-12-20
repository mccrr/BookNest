using BookNest.Models.Entities;
using System.ComponentModel;
using BookNest.Utils;
using BookNest.DataAccess;

namespace BookNest.Services
{
    public class ShelfBookService
    {
        private readonly ShelfBookDao _shelfBookDao;
        private readonly BookService _bookService;
        private readonly ShelfService _shelfService;
        ShelfBookService(ShelfBookDao shelfBookDao, BookService bookService, ShelfService shelfService)
        {
            _shelfBookDao = shelfBookDao;
            _bookService = bookService;
            _shelfService = shelfService;
        }

        public async Task<ShelfBook> Add(string isbn, int shelfId)
        {
            var dbBook = await _bookService.GetById(isbn);
            var dbShelf = await _shelfService.GetById(shelfId);

            var shelfBook = new ShelfBook(isbn,shelfId);
            var dbShelfBook = await _shelfBookDao.AddAsync(shelfBook);
            if (dbShelfBook == null) throw new CustomException("Book couldnt be added to shelf");
            return dbShelfBook;

        }

        public async Task<ShelfBook> GetByKey(string isbn, int shelfId)
        {
            var shelfBook = await _shelfBookDao.FindByKey(isbn, shelfId);
            if (shelfBook == null) throw new NotFoundException($"Book with isbn: {isbn} isnt in shelf: {shelfId}");
            return shelfBook;
        }

        public async Task Remove(string isbn, int shelfId)
        {
            var dbBook = await (_bookService.GetById(isbn));
            var dbShelf = await _shelfService.GetById(shelfId);
            var shelfBook = await GetByKey(isbn, shelfId);
            await _shelfBookDao.Remove(shelfBook);
        }
    }
}
