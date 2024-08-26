using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.ProjectManagement.WebApi.Domain.Services
{
    public interface IProjectService
    {
        Task<Project> GetProjectByIdAsync(Guid id);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task CreateProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(Guid id);
    }
}
