using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using static DAL.Helper.UserEntityTranslator;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Username == username)
                .ContinueWith(task => task.Result != null ? ToUser(task.Result) : null);
        }

        public async Task AddUserAsync(User user)
        {
            var userEntity = ToEntity(user);
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();
        }
    }
}
