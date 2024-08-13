using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ProductApi.Core.Entities;
using ProductApi.Core.Interfaces;
using ProductApi.Core.Repositories;
using ProductApi.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductDbContext _context;

        public UserRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
