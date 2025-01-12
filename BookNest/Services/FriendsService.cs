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

        public async Task<List<int>> GetAllFriends(int userId)
        {
            var friendsIdList = new List<int>();
            var result = await _friendsDao.GetAllFriends(userId);
            foreach (var friend in result) {
                if (friend.UserId == userId) friendsIdList.Add(friend.FriendId);
                else friendsIdList.Add(friend.UserId);
            }
            return friendsIdList;
        }


        public async Task<List<FriendRequestResponseDto>> GetSentRequestsByUser(int userId)
        {
            var result = await _friendsDao.GetSentRequestByUser(userId);
            var responseList = new List<FriendRequestResponseDto>();
            var dbFriend = new User();
            foreach (var friend in result) { 
                dbFriend = await _userService.GetById(friend.ReceiverId);
                responseList.Add(new FriendRequestResponseDto(friend, dbFriend.Username, dbFriend.Avatar));
            }
            return responseList;
        }

        public async Task<List<FriendRequestResponseDto>> GetReceivedRequestsByUser(int userId)
        {
            var result = await _friendsDao.GetReceivedRequestByUser(userId);
            var responseList = new List<FriendRequestResponseDto>();
            var dbFriend = new User();
            foreach (var friend in result)
            {
                dbFriend = await _userService.GetById(friend.SenderId);
                responseList.Add(new FriendRequestResponseDto(friend, dbFriend.Username, dbFriend.Avatar));
            }
            return responseList;
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
