using Microsoft.AspNetCore.Http.HttpResults;

namespace BookNest.Models.Entities
{
    public class Mute
    {
        public Mute() { }
        public int MuterId { get; set; }
        public required User Muter { get; set; }

        public int MutedId { get; set; }
        public required User Muted { get; set; } 

        public DateTime CreatedAt {  get; set; } = DateTime.Now;
    }
}
