using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.ProjectManagement.WebApi.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> GetByIdAsync(Guid id);
        Task<IEnumerable<Project>> GetAllAsync();
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(Guid id);
    }
}
 