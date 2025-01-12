using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookNest.DataAccess
{
    public class FriendsDao
    {
        private readonly ApplicationDbContext _context;
        public FriendsDao(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Friend>> GetAllFriends(int userId)
        {
            return await _context.Friends
                .Where(x => x.UserId == userId || x.FriendId == userId)
                .ToListAsync();
        }

        public async Task<FriendRequest> SendRequest(FriendRequest friendRequest)
        {
            var result = await _context.AddAsync(friendRequest);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<FriendRequest> GetRequestByKey(int senderId, int receiverId)
        {
            return await _context.FriendRequests
                .FirstOrDefaultAsync(fr => fr.SenderId==senderId && fr.ReceiverId==receiverId);
        }
        public async Task<List<FriendRequest>> GetReceivedRequestByUser(int userId)
        {
            return _context.FriendRequests
                .Where(fr => fr.ReceiverId == userId).ToList();
        }
        public async Task<List<FriendRequest>> GetSentRequestByUser(int userId)
        {
            return _context.FriendRequests
                .Where(fr => fr.SenderId == userId).ToList();
        }

        public async Task<Friend> GetFriend(int user1, int user2)
        {
            return await _context.Friends
                .FirstOrDefaultAsync(fr => (fr.UserId == user1 && fr.FriendId == user2) 
                || (fr.UserId == user2 && fr.FriendId == user1));
        }

        public async Task DeleteRequest(FriendRequest request)
        {
            _context.FriendRequests.Remove(request);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveFriend(Friend friend)
        {
            _context.Friends.Remove(friend);
            await _context.SaveChangesAsync();
        }
        public async Task<Friend> AddFriend(Friend friend)
        {
            var dbFriend = await _context.Friends.AddAsync(friend);
            await _context.SaveChangesAsync();
            return dbFriend.Entity;
        }
    }
}
