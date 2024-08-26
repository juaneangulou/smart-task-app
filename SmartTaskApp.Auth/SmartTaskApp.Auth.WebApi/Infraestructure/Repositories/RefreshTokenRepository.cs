using Microsoft.EntityFrameworkCore;
using SmartTaskApp.Auth.WebApi.Domain.Repositories;
using SmartTaskApp.CommonDb;
using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.Auth.WebApi.Infraestructure.Data
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly SmartTaskAppDbContext _context;

        public RefreshTokenRepository(SmartTaskAppDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task RevokeAsync(RefreshToken refreshToken)
        {
            refreshToken.Revoke();
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(RefreshToken refreshToken)  
        {
            _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync();
        }
    }
}
