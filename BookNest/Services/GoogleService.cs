using BookNest.Dtos.Books;
using BookNest.Models.Entities;
using BookNest.Utils;
using System.Text.Json;

namespace BookNest.Services
{
    public class GoogleService
    {
        private readonly HttpClient _httpClient;
        private readonly BookService _bookService;

        public GoogleService(HttpClient httpClient, BookService bookService)
        {
            _httpClient = httpClient;
            _bookService = bookService;
        }

        public async Task<Book> GetBookInfoAsync(string isbn)
        {
            var response = await _httpClient.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}&fields=items(volumeInfo(title,authors,description,pageCount,imageLinks/thumbnail,publishedDate,publisher,categories,language))");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // Parse and map the JSON response to a Book object
                return await ParseBookResponseAsync(content,isbn);
            }
            else
            {
                // Handle failed request...
                throw new CustomException("Failed to retrieve book information");
            }
        }

        public async Task<Book> ParseBookResponseAsync(string jsonResponse, string isbn)
        {
            var document = JsonDocument.Parse(jsonResponse);

            var volumeInfo = document.RootElement
                .GetProperty("items")[0]
                .GetProperty("volumeInfo");

            DateTime date;
            bool isDateValid = DateTime.TryParse(volumeInfo.GetProperty("publishedDate").GetString(), out date);
            if (isDateValid)
            {
                Console.WriteLine(date.ToString());
            }

            Author author;
            var authorName = volumeInfo.GetProperty("authors").EnumerateArray().FirstOrDefault().GetString();
            var existingAuthor = await _bookService.GetAuthorByName(authorName);
            if (existingAuthor != null)
                author = existingAuthor;
            else
            {
                var newAuthor = await _bookService.AddAuthor(authorName);
                author = newAuthor;
            }
            var cover = volumeInfo.GetProperty("imageLinks").GetProperty("thumbnail").GetString();
            if (cover.StartsWith("http://"))
            {
                cover = "https://" + cover.Substring(7); // Replace "http://" with "https://"
            };
            Console.WriteLine($"Cover: {cover}");

            // Map to the Book class
            var book = new Book
            {
                Isbn = isbn,
                Title = volumeInfo.GetProperty("title").GetString(),
                AuthorId = author.Id,
                Publisher = volumeInfo.GetProperty("publisher").GetString(),
                PublishedDate = date,
                Description = volumeInfo.GetProperty("description").GetString(),
                Pages = volumeInfo.GetProperty("pageCount").GetInt32(),
                Category = volumeInfo.GetProperty("categories").EnumerateArray().FirstOrDefault().GetString() ?? "Unknown",
                Cover = cover,
                Language = volumeInfo.GetProperty("language").GetString()
            };

            return book;
        }
    }
}
