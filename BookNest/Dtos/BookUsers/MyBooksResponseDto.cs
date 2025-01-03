namespace BookNest.Dtos.BookUsers
{
    public class MyBooksResponseDto
    {
        public List<MyBooksDto> Reading { get; set; } = new List<MyBooksDto>();
        public List<MyBooksDto> Read { get; set; } = new List<MyBooksDto>();
        public List<MyBooksDto> WantToRead{ get; set; } = new List<MyBooksDto>();
    }
}
