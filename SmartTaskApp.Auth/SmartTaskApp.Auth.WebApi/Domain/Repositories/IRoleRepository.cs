namespace SmartTaskApp.Auth.WebApi.Domain.Repositories
{
    public interface IRoleRepository
    {
        Task<string> GetByNameAsync(string roleName);
        Task AddAsync(string roleName);
    }
}
