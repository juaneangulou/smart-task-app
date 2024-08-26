using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.Auth.WebApi.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken refreshToken);
        Task RevokeAsync(RefreshToken refreshToken);
        Task RemoveAsync(RefreshToken refreshToken);
    }
}
