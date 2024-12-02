using System.Net;

namespace BookNest.Utils
{
    public interface IBaseResponse
    {
        bool Success { get; set; }
        string? Message { get; set; }
        object? Body { get; }
        HttpStatusCode Status { get; set; }
    }
}