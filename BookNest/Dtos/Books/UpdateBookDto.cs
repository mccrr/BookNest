namespace BookNest.Dtos.Books
{
    public class UpdateBookDto
    {
        public string? Title { get; set; }
        public int? Pages { get; set; }
        public string? Cover { get; set; }
        public string? Description { get; set; }
        public int? AuthorId { get; set; }
    }
}
