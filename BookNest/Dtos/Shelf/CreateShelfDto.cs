using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos.Shelf
{
    public class CreateShelfDto
    {
        public required string Name { get; set; }
        public required int UserId { get; set; }
    }
}
