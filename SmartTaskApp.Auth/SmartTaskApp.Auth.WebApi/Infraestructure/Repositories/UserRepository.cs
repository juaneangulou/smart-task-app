using Microsoft.EntityFrameworkCore;
using SmartTaskApp.Auth.WebApi.Domain.Repositories;
using SmartTaskApp.CommonDb;
using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.Auth.WebApi.Infraestructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly SmartTaskAppDbContext _context;
        public UserRepository(SmartTaskAppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}


