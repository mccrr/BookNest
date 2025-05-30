﻿using BookNest.Data;
using BookNest.Models.Entities;
using BookNest.Utils;
using Microsoft.EntityFrameworkCore;
using BookNest.Dtos.Users;

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
            return await _context.Users.FirstOrDefaultAsync(u => u.Id==id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
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
            if (user == null)  throw new NotFoundException($"User with id {id} doesnt exist");
            if (updateUserDto.FirstName != null) user.FirstName = updateUserDto.FirstName;
            if (updateUserDto.LastName != null) user.LastName = updateUserDto.LastName;
            if (updateUserDto.Avatar != null) user.Avatar = updateUserDto.Avatar;
            if (updateUserDto.Age.HasValue) user.Age = updateUserDto.Age.Value;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new NotFoundException($"User with id {id} doesnt exist");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        
    }
}
