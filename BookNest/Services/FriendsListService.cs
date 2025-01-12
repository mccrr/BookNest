using BookNest.DataAccess;

namespace BookNest.Services
{
    public class FriendsListService
    {
        private readonly FriendsDao _friendsDao;
        public FriendsListService(FriendsDao friendsDao) { 
            _friendsDao = friendsDao;
        }

        public async Task<List<int>> GetAllFriends(int userId)
        {
            var friendsIdList = new List<int>();
            var result = await _friendsDao.GetAllFriends(userId);
            foreach (var friend in result)
            {
                if (friend.UserId == userId) friendsIdList.Add(friend.FriendId);
                else friendsIdList.Add(friend.UserId);
            }
            return friendsIdList;
        }
    }
}
