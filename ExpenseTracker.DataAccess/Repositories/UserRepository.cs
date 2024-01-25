using ExpenseTracker.Common.Models;
using ExpenseTracker.DataAccess.DbContexts;
using ExpenseTracker.Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> AddUserAsync(User user)
        {
            return await InsertAsync(user);
        }
    }
}
