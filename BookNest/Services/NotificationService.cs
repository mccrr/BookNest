using BookNest.DataAccess;
using BookNest.Dtos.Notifications;
using BookNest.Models.Entities;
using BookNest.Utils;

namespace BookNest.Services
{
    public class NotificationService
    {
        private readonly NotificationDao _notificationDao;
        private readonly FriendsListService _friendsService;
        private readonly ForbiddenService _forbiddenService;
        public NotificationService(NotificationDao notificationDao, FriendsListService friendsService,ForbiddenService forbiddenService)
        {
            _notificationDao = notificationDao;
            _friendsService = friendsService;
            _forbiddenService = forbiddenService;
        }

        public async Task<Notification> Create(NotificationDto dto)
        {
            var notification = new Notification(dto);
            var dbNotification = await _notificationDao.AddAsync(notification);
            if (dbNotification == null) throw new CustomException("Notification couldnt be created!");
            if (dbNotification.Type.ToLower() == "friends")
            {
                var un1 = await _notificationDao.CreateUserNotification(dto.UserId, dbNotification.Id);                
                var un2 = await _notificationDao.CreateUserNotification(dto.OtherId ?? 0, dbNotification.Id);
                
            }
            else if(dbNotification.Type.ToLower() == "bookprogress")
            {
                var friendsList = await _friendsService.GetAllFriends(dbNotification.UserId);
                foreach (var friend in friendsList) {
                    await _notificationDao.CreateUserNotification(friend, dbNotification.Id);
                }
            }
            return dbNotification;
        }

        public async Task<Notification> GetById(int id)
        {
            var notification = await _notificationDao.GetById(id);
            if (notification == null) throw new NotFoundException($"Notification with id:{id} not found!");
            return notification;
        }

        public async Task<List<NotifResDto>> GetAllUserNotifications(int userId)
        {
            var result = new List<NotifResDto>();
            Console.WriteLine($"USerId: {userId}");
            var notifications = await _notificationDao.GetAllUserNotifications(userId);
            foreach (var notification in notifications) {
                var fullnoti = await _notificationDao.GetById(notification.NotificationId);
                if (fullnoti == null) throw new NotFoundException($"fullnoti with id: {fullnoti.Id} doesnt exist");
                Console.WriteLine($"fullnoti: {fullnoti.Id} {fullnoti.BookId} userId: {fullnoti.UserId} type:{fullnoti.Type} - otherId: {fullnoti.OtherId}");
                var res = new NotifResDto();
                var otherUser = new User();
                if (fullnoti.Type == "friends") {
                    if (fullnoti.UserId == userId)
                    {
                        otherUser = await _forbiddenService.GetUserByIdAsync(fullnoti.OtherId ?? 0);
                    }
                    else otherUser = await _forbiddenService.GetUserByIdAsync(fullnoti.UserId);
                    res = new NotifResDto(otherUser.Id, otherUser.Username, otherUser.Avatar,null, null, null, null, null, "friends", 0,fullnoti.CreatedAt.ToString("O"));
                }
                else if(fullnoti.Type == "bookprogress")
                {
                    otherUser = await _forbiddenService.GetUserByIdAsync(fullnoti.UserId);
                    Console.WriteLine($"fullnoti: {fullnoti.Id} {fullnoti.BookId} - otherId: {fullnoti.OtherId}");
                    var bookprogress = await _forbiddenService.FindBookProgress(fullnoti.UserId,fullnoti.BookId);
                    var book = await _forbiddenService.GetBookById(bookprogress.BookId);
                    var author = await _forbiddenService.GetAuthorById(book.AuthorId);
                    res = new NotifResDto(otherUser.Id, otherUser.Username, otherUser.Avatar, bookprogress.BookId,
                        book.Title, author.Name, book.Cover, bookprogress.Status, "bookprogress", bookprogress.Progress,fullnoti.CreatedAt.ToString("O"));
                }
                result.Add(res);
            }
            return result;
        }

        public async Task Delete(int id)
        {
            var notification = await GetById(id);
            await _notificationDao.Delete(notification);
        }
    }
}
