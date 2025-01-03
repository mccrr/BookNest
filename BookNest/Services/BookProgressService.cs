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
            await _bookService.GetById(bookUserDto.BookId);
            if (bookUserDto.Status.ToLower().Equals("read") && bookUserDto.Progress != 100)
                throw new CustomException("Book status says 'read' but progress is not maximum!");
            if (bookUserDto.Status.ToLower().Equals("wanttoread") && bookUserDto.Progress != 0)
                throw new CustomException("Book status says 'wantoread' but progress is not 0");
            var dbBookUser = await _bookUserDao.FindByKey(userId,bookUserDto.BookId,bookUserDto.Progress);
            if (dbBookUser != null)
            {
                Console.WriteLine($"dbBookUser Progress: {dbBookUser.Progress}");
                if (dbBookUser.Progress == bookUserDto.Progress) throw new CustomException("Progress has already been submitted");
                else if (dbBookUser.Progress > bookUserDto.Progress)
                    throw new CustomException("New progress is lower than previously submitted progress");
            }
            if (!(bookUserDto.Status.ToLower().Equals("read") || bookUserDto.Status.ToLower().Equals("wanttoread")
                || bookUserDto.Status.ToLower().Equals("reading")))
                throw new CustomException("Invalid status");
            var bookUser = new BookUser(bookUserDto,userId);
            var newDbBookUser = await _bookUserDao.Add(bookUser);
            if (newDbBookUser == null) throw new CustomException("Book progress couldnt be added!");
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
            var responseDto = new MyBooksResponseDto();
            foreach (var mybook in mybooksList)
            {
                var book = await _bookService.GetById(mybook.BookId);
                var dto = new MyBooksDto(mybook, book.Cover,book.Title);
                if (dto.Status == "reading") responseDto.Reading.Add(dto);
                else if (dto.Status == "read") responseDto.Read.Add(dto);
                else responseDto.WantToRead.Add(dto);
            }
            return responseDto;
        }

        //public async Task<List<BookUser>> GetMax(int userId)
        //{
        //    var progressList = await _bookUserDao.GetMax(userId);
        //    return progressList;
        //}
    }
}
