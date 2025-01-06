using BookNest.DataAccess;
using BookNest.Dtos.BookUsers;
using BookNest.Models.Entities;
using BookNest.Utils;

namespace BookNest.Services
{
    public class BookProgressService
    {
        private readonly BookUserDao _bookUserDao;
        private readonly BookService _bookService;
        public BookProgressService(BookUserDao bookUserDao, BookService bookService)
        {
            _bookUserDao = bookUserDao;
            _bookService = bookService;
        }

        public async Task<BookUserDto> Create(BookUserDto bookUserDto, int userId)
        {
            await _bookService.GetById(bookUserDto.BookId, true);
            bookUserDto.Status = bookUserDto.Status.ToLower();
            if (bookUserDto.Status.ToLower().Equals("read") && bookUserDto.Progress != 100)
                throw new CustomException("Book status says 'read' but progress is not maximum!");

            if (bookUserDto.Status.ToLower().Equals("wanttoread") && bookUserDto.Progress != 0)
                throw new CustomException("Book status says 'wantoread' but progress is not 0");
            var dbBookUser = await _bookUserDao.FindByKey(userId,bookUserDto.BookId);

            if (dbBookUser != null)
            {
                if (dbBookUser.Status == "reading")
                {
                    if (dbBookUser.Progress == bookUserDto.Progress) throw new CustomException("Progress has already been submitted");
                    else if (dbBookUser.Progress > bookUserDto.Progress)
                        throw new CustomException("New progress is lower than previously submitted progress");
                }
                else throw new CustomException("Progress has already been submitted");
            }

            var bookUser = new BookUser(bookUserDto,userId);
            var newDbBookUser = await _bookUserDao.Add(bookUser);
            if (newDbBookUser == null) throw new CustomException("Book progress couldnt be added!");

            if (!(bookUserDto.Status.ToLower().Equals("read") || bookUserDto.Status.ToLower().Equals("wanttoread")
                || bookUserDto.Status.ToLower().Equals("reading")))
                throw new CustomException("Invalid status");

            if (bookUserDto.Status == "wanttoread")
            {
                var existingReadingList = await _bookUserDao.GetAllReading(userId, bookUserDto.BookId);
                if (existingReadingList != null)
                {
                    foreach (var exReading in existingReadingList)
                    {
                        await _bookUserDao.Delete(exReading);
                    }
                }
            }
            if (bookUserDto.Status == "wanttoread" || bookUserDto.Status == "reading")
            {
                var existingRead = await _bookUserDao.GetBookStatus(userId, bookUserDto.BookId, "read");
                if (existingRead != null) await _bookUserDao.Delete(existingRead);
            }

            return new BookUserDto(newDbBookUser);
        }

        //public async Task<List<BookUser>> GetByUser(int userId)
        //{
        //    var progressList = await _bookUserDao.GetByUser(userId);
        //    return progressList;
        //}
        public async Task<MyBooksResponseDto> GetMyBooks(int userId)
        {
            var mybooksList = await _bookUserDao.GetByUser(userId);
            var readList = mybooksList.Where(x => x.Status == "read");
            var readingList = mybooksList.Where(x => x.Status == "reading");
            var responseDto = new MyBooksResponseDto();
            foreach (var mybook in mybooksList)
            {
                var book = await _bookService.GetById(mybook.BookId, false);
                if (book is null) continue;
                var dto = new MyBooksDto(mybook, book.Cover,book.Title);
                if (dto.Status.ToLower().Equals("read")) responseDto.Read.Add(dto);
                else if (dto.Status.ToLower().Equals("reading") && !readList.Any(x => x.BookId == dto.BookId))
                    responseDto.Reading.Add(dto);  
                else if (dto.Status.ToLower().Equals("wanttoread") && !readList.Any(x => x.BookId == dto.BookId)
                    && !readingList.Any(x => x.BookId == dto.BookId)) 
                    responseDto.WantToRead.Add(dto); 
            }
            return responseDto;
        }

        public async Task RemoveMyBook(int userId, string isbn)
        {
            var progressList = await _bookUserDao.GetAllByUserAndBook(userId, isbn);
            foreach (var progress in progressList)
            {
                await _bookUserDao.Delete(progress);
            }
        }

        //public async Task<List<BookUser>> GetMax(int userId)
        //{
        //    var progressList = await _bookUserDao.GetMax(userId);
        //    return progressList;
        //}
    }
}
