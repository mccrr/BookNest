using BookNest.DataAccess;
using BookNest.Dtos.Friends;
using BookNest.Dtos.Notifications;
using BookNest.Models.Entities;
using BookNest.Utils;

namespace BookNest.Services
{
    public class FriendsService
    {
        private readonly FriendsDao _friendsDao;
        private readonly UserService _userService;
        private readonly NotificationService _notificationService;
        public FriendsService(FriendsDao friendsDao, UserService userService, NotificationService notificationService)
        {
            _friendsDao = friendsDao;
            _userService = userService;
            _notificationService = notificationService;
        }


        public async Task<List<FriendRequestDto>> GetSentRequestsByUser(int userId)
        {
            var result = await _friendsDao.GetSentRequestByUser(userId);
            return result.Select(x => new FriendRequestDto(x)).ToList();
        }

        public async Task<List<FriendRequestDto>> GetReceivedRequestsByUser(int userId)
        {
            var result = await _friendsDao.GetReceivedRequestByUser(userId);
            return result.Select(x => new FriendRequestDto(x)).ToList();
        }
        public async Task<FriendRequest> SendRequest(int userId, int friendId)
        {
            var friendship = await _friendsDao.GetFriend(userId, friendId);
            if (friendship != null) throw new CustomException("You're already friends");
            var dbFriend = await _userService.GetById(friendId);
            var frequest = new FriendRequest(userId, friendId);
            var dbFrequest = await _friendsDao.SendRequest(frequest);
            if (dbFrequest == null) throw new CustomException("Couldnt send friend request.");
            return dbFrequest;
        }

        public async Task RemoveRequest(int sender, int receiver)
        {
            var request = await GetRequestByKey(sender, receiver, true);
            await _friendsDao.DeleteRequest(request);
        }
        public async Task RemoveFriend(int sender, int receiver)
        {
            var friendship = await GetFriend(sender, receiver,true);
            await _friendsDao.RemoveFriend(friendship);
        }
        public async Task<FriendRequest> GetRequestByKey(int sender, int receiver, bool throwError)
        {
            var request = await _friendsDao.GetRequestByKey(sender, receiver);
            if (request == null && throwError)
                    throw new NotFoundException("Friend Request not found.");
            return request;
        }

        public async Task<Friend> GetFriend(int sender, int receiver, bool throwError)
        {
            var request = await _friendsDao.GetFriend(sender, receiver);
            if (request == null)
            {
                if (throwError)
                    throw new NotFoundException("Friend not found.");
            }
            return request;
        }

        public async Task<Friend?> SendResponse(FRequestResponseDto dto, int userId)
        {
            Console.WriteLine($"SenderId: {dto.SenderId}\nReceiverId: {userId}");
            var request = await GetRequestByKey(dto.SenderId, userId, false);
            if (dto.Response == false)
            {
                await _friendsDao.DeleteRequest(request);
                return null;
            }

            await _friendsDao.DeleteRequest(request);
            var friend = new Friend(dto.SenderId, userId);
            var createdFriends = await _friendsDao.AddFriend(friend);
            if (createdFriends != null)
            {
                var newNotifdto = new NotificationDto(userId, dto.SenderId, null, "friends", "You are now friends");
                var newNotif = await _notificationService.Create(newNotifdto);
                Console.WriteLine($"New Notif: {newNotif.Id} {newNotif.UserId} {newNotif.OtherId} {newNotif.Type} {newNotif.Text}");
            }
            return createdFriends;
        }
    }
}
