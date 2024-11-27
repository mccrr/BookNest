using BookNest.Data;
using BookNest.Dtos;
using BookNest.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookNest.DataAccess
{
    public class UserDao
    {
        private readonly ApplicationDbContext _context;
        public UserDao(ApplicationDbContext context) { 
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> AddAsync(User user)
        {
            var dbUser = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return dbUser.Entity;
        }

        public async Task<User> UpdateAsync(UpdateUserDto updateUserDto, int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (updateUserDto.FirstName != null) user.FirstName = updateUserDto.FirstName;
            if (updateUserDto.LastName != null) user.LastName = updateUserDto.LastName;
            if (updateUserDto.Avatar != null) user.Avatar = updateUserDto.Avatar;
            if (updateUserDto.Age.HasValue) user.Age = updateUserDto.Age.Value;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        
    }
}
