using BookNest.DataAccess;
using BookNest.Models.Entities;
using BookNest.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel;

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

        public async Task<User> CreateUser(SignUpDto signUpDto)
        {
            var user = new User(signUpDto);
            var dbUser = await userDao.AddAsync(user);
            return dbUser;
        }

        //public async Task<User> UpdateUser(UpdateUserDto updateUserDto)
        //{
        //    //TODO: Get user from accesstoken
        //    var dbUser = await userDao.UpdateAsync(updateUserDto, user.id)
        //    return dbUser;
        //}

        //public async void DeleteUser(int id)
        //{
        //    var user = await userDao.GetByIdAsync(id);
        //    if (user == null) return 
        //}

    }
}
