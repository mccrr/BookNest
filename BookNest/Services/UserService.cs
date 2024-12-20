using BookNest.DataAccess;
using BookNest.Models.Entities;
using BookNest.Utils;
using BookNest.Dtos.Users;

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
            var existingEmail = await userDao.GetByEmailAsync(signUpDto.Email);
            if (existingEmail!= null) throw new CustomException("There already is an account with this email!");
            var existingUsername = await userDao.GetByUsernameAsync(signUpDto.Username);
            if (existingUsername != null) throw new CustomException("Username is not available!");
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
