using BookNest.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Achievement> Achievements{ get; set; }
        public DbSet<Author> Authors{ get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookUser> BookUsers{ get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupBook> GroupBooks { get; set; }
        public DbSet<GroupRequest> GroupRequests { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Mute> Mutes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<ShelfBook> ShelfBooks{ get; set; }
        public DbSet<UserAchievement> UserAchievements{ get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///// UserAchievement relationships
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserAchievement>()
                .HasKey(ua => new { ua.UserId, ua.AchievementId });

            modelBuilder.Entity<UserAchievement>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAchievements)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserAchievement>()
                .HasOne(ua => ua.Achievement)
                .WithMany(a => a.UserAchievements)
                .HasForeignKey(ua => ua.AchievementId)
                .OnDelete(DeleteBehavior.Cascade);

            ///// UserNotification relationships
            modelBuilder.Entity<UserNotification>()
                .HasKey(un => new { un.NotificationId, un.UserId });

            modelBuilder.Entity<UserNotification>()
                .HasOne(un => un.Notification)
                .WithMany(n => n.UserNotifications)
                .HasForeignKey(ua => ua.NotificationId);

            modelBuilder.Entity<UserNotification>()
                .HasOne(un => un.User)
                .WithMany(u => u.UserNotifications)
                .HasForeignKey(un => un.UserId);

            //// BookUser relationships
            modelBuilder.Entity<BookUser>()
                .HasKey(bu => new { bu.UserId, bu.BookId });

            modelBuilder.Entity<BookUser>()
                .HasOne(bu=>bu.Book)
                .WithMany(b=>b.BookUsers)
                .HasForeignKey(bu=>bu.BookId)
                .HasPrincipalKey(b=>b.Isbn)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookUser>()
                .HasOne(bu => bu.User)
                .WithMany(u => u.BookUsers)
                .HasForeignKey(bu => bu.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            ////Mute relationships
            modelBuilder.Entity<Mute>()
                .HasKey(m => new { m.MuterId, m.MutedId });

            modelBuilder.Entity<Mute>()
                .HasOne(m => m.Muted)
                .WithMany(u => u.Muted)
                .HasForeignKey(m => m.MutedId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Mute>()
                .HasOne(m => m.Muter)
                .WithMany(u => u.Muter)
                .HasForeignKey(m => m.MuterId)
                .OnDelete(DeleteBehavior.Cascade);

            ////Friend relationships
            modelBuilder.Entity<Friend>()
                .HasKey(f => new { f.UserId, f.FriendId});

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.User)
                .WithMany(u => u.FriendsInitiated)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.Friendd)
                .WithMany(u => u.FriendsReceived)
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Cascade);


            ///// FriendRequest relationships
            modelBuilder.Entity<FriendRequest>()
                .HasKey(fr => new { fr.SenderId, fr.ReceiverId });

            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Sender)
                .WithMany(f => f.SentFriendRequests)
                .HasForeignKey(f=>f.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Receiver)
                .WithMany(f => f.ReceivedFriendRequests)
                .HasForeignKey(f => f.ReceiverId)
                .OnDelete(DeleteBehavior.Cascade);

            //// GroupBook relationships
            // GroupBook relationships
            modelBuilder.Entity<GroupBook>()
                .HasKey(gb => new { gb.BookId, gb.UserId, gb.GroupId }); // Composite key for GroupBook

            modelBuilder.Entity<GroupBook>()
                .HasOne(gb => gb.Book)
                .WithMany(b => b.GroupBooks)
                .HasForeignKey(gb => gb.BookId)
                .HasPrincipalKey(b => b.Isbn)
                .OnDelete(DeleteBehavior.Cascade); // Matches Book's primary key

            modelBuilder.Entity<GroupBook>()
                .HasOne(gb => gb.GroupUser)
                .WithMany(gu => gu.GroupBooks)
                .HasForeignKey(gb => new { gb.UserId, gb.GroupId }) // Composite foreign key
                .HasPrincipalKey(gu => new { gu.UserId, gu.GroupId })
                .OnDelete(DeleteBehavior.Cascade); // Matches GroupUser's composite key


            //// GroupRequest relationships
            modelBuilder.Entity<GroupRequest>()
                .HasKey(gr => new { gr.UserId, gr.GroupId });

            modelBuilder.Entity<GroupRequest> ()
                .HasOne(gr => gr.Group)
                .WithMany(g => g.GroupRequests)
                .HasForeignKey(gr => gr.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupRequest>()
                .HasOne(gr => gr.User)
                .WithMany(u => u.GroupRequests)
                .HasForeignKey(gr => gr.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            //// GroupUser relationships
            modelBuilder.Entity<GroupUser>()
                .HasKey(gu => new {gu.UserId, gu.GroupId});

            modelBuilder.Entity <GroupUser>()
                .HasOne(gu => gu.User)
                .WithMany(u => u.GroupUsers)
                .HasForeignKey(gu => gu.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupUser> ()  
                .HasOne (gu => gu.Group)
                .WithMany(g => g.GroupUsers)
                .HasForeignKey(gu => gu.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            //// ShelfBook relationships
            modelBuilder.Entity<ShelfBook>()
                .HasKey(sb => new { sb.ShelfId, sb.BookId });

            modelBuilder.Entity<ShelfBook> ()
                .HasOne(sb => sb.Shelf)
                .WithMany(s => s.ShelfBooks)
                .HasForeignKey(sb => sb.ShelfId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShelfBook>()
                .HasOne(sb => sb.Book)
                .WithMany(s => s.ShelfBooks)
                .HasForeignKey(sb => sb.BookId)
                .HasPrincipalKey(b => b.Isbn)
                .OnDelete(DeleteBehavior.Cascade);

            /// RefreshToken relationships
            modelBuilder.Entity<RefreshToken>()
                .HasKey(rt => rt.UserId);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany()
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        }
}
