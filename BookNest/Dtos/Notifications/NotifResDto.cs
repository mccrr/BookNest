namespace BookNest.Dtos.Notifications
{
    public class NotifResDto
    {
        public NotifResDto() { }
       public NotifResDto(
       int userId,
       string userName,
       string userAvatar,
       string bookId,
       string bookTitle,
       string author,
       string bookCover,
       string status,
       string type,
       int progress,
       string createdAt)
        {
            UserId = userId;
            UserName = userName;
            UserAvatar = userAvatar;
            Type = type;
            BookId = bookId;
            BookTitle = bookTitle;
            Author = author;
            BookCover = bookCover;
            Status = status;
            Progress = progress;
            CreatedAt = createdAt;
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public string Type { get; set; }
        public string? BookId { get; set; }
        public string? BookTitle { get; set; }
        public string? Author { get; set; }
        public string? BookCover { get; set; }
        public string? Status { get; set; }
        public int? Progress { get; set; }
        public string CreatedAt { get; set; }
    }
}
