using BookNest.DataAccess;
using BookNest.Dtos.BookUsers;
using BookNest.Models.Entities;
using BookNest.Utils;

namespace BookNest.Services
{
    public class BookProgressService
    {
        private readonly BookUserDao _bookUserDao;
        public BookProgressService(BookUserDao bookUserDao) {
            _bookUserDao = bookUserDao;
        }

        public async Task<BookUser> Create(BookUserDto bookUserDto)
        {
            var bookUser = new BookUser(bookUserDto);
            var dbBookUser = await _bookUserDao.Add(bookUser);
            if (dbBookUser == null) throw new CustomException("Book progress couldnt be added!");
            return dbBookUser;
    }
}
