using BookNest.DataAccess;
using BookNest.Models.Entities;
using BookNest.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using BookNest.Utils;

namespace BookNest.Services
{
    public class UserService
    {
        private readonly UserDao userDao;
        public UserService(UserDao userDao) { 
            this.userDao = userDao;
        }

        public async Task<List<User>> GetUsers(CancellationToken cancellationToken)
        {
            return await userDao.GetUsersAsync(cancellationToken);
        }

        public async Task<User> GetById(int id)
        {
            return await userDao.GetByIdAsync(id);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await userDao.GetByEmailAsync(email);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await userDao.GetByUsernameAsync(username);
        }
        public async Task<User> CreateUser(SignUpDto signUpDto)
        {
            signUpDto.Password = HashPassword(signUpDto.Password);
            var user = new User(signUpDto);
            var dbUser = await userDao.AddAsync(user);
            return dbUser;
        }

        public async Task<User> UpdateUser(UpdateUserDto updateUserDto, int id)
        {
            var dbUser = await userDao.UpdateAsync(updateUserDto, id);
            return dbUser;
        }

        public async Task DeleteUser(int id)
        {
            await userDao.DeleteAsync(id);
        }


        public string HashPassword(string plainTextPassword)
            {
                // Generate a salt
                string salt = BCrypt.Net.BCrypt.GenerateSalt();

                // Hash the plain text password with the generated salt
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainTextPassword, salt);

                return hashedPassword;
            }

        public bool VerifyPassword(string plainTextPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
        }


        

    }
}
