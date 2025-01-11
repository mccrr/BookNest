using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BookNest.Models.Entities;
using BookNest.Dtos.BookUsers;

namespace BookNest.Dtos.Users
{
    public class UserDto
    {
        public UserDto(User user, bool pendingSent, bool pendingReceived, bool isFriend, MyBooksResponseDto dto)
        {
            Id = user.Id;
            Username = user.Username;
            Avatar = user.Avatar;
            IsFriend = isFriend;
            PendingReceivedRequest = pendingReceived;
            PendingSentRequest = pendingSent;
            Reading = dto.Reading;
            WantToRead = dto.WantToRead;
            Read = dto.Read;

        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
        public List<MyBooksDto> Reading { get; set; } = new List<MyBooksDto>();
        public List<MyBooksDto> Read { get; set; } = new List<MyBooksDto>();
        public List<MyBooksDto> WantToRead { get; set; } = new List<MyBooksDto>();
        public bool PendingSentRequest { get; set; }
        public bool PendingReceivedRequest { get; set; }
        public bool IsFriend { get; set; }

    }
}
