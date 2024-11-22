using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Username { get; set; }
        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set;}
        [Required]
        [MaxLength(50)]
        public required string LastName { get; set;}
        [Required]
        public required int Age { get; set;}
        [Required]
        [EmailAddress]
        public required string Email { get; set;}
        [Required]
        public required string Password { get; set;}
        [StringLength(255)]
        [DefaultValue("https://www.shutterstock.com/search/blank-profile-picture")]
        public string Avatar { get; set; } = "https://www.shutterstock.com/search/blank-profile-picture";
        public DateTime CreatedAt { get; set;} = DateTime.Now;

        public ICollection<BookUser>? BookUsers { get; set;}
    }
}
