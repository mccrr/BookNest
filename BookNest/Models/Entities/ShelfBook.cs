namespace BookNest.Models.Entities
{
    public class ShelfBook
    {
        public ShelfBook(string isbn, int shelfId)
        {
            BookId= isbn;
            ShelfId= shelfId;
        }
        public ShelfBook() { }
        public string BookId { get; set; }
        public Book Book { get; set; }
        public int ShelfId { get; set; }
        public Shelf Shelf { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.Now;
    }
}
