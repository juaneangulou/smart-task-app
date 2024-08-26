using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.Auth.WebApi.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string userId);
        Task AddAsync(User user);
        Task SaveAsync(User user);
        Task DeleteAsync(User user);
    }
}
