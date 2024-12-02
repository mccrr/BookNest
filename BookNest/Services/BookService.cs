using BookNest.DataAccess;
using BookNest.Models.Entities;

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
    }
}
