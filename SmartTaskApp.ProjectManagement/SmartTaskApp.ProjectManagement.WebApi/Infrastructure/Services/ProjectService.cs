using SmartTaskApp.CommonDb.Entities;
using SmartTaskApp.ProjectManagement.WebApi.Domain.Repositories;
using SmartTaskApp.ProjectManagement.WebApi.Domain.Services;

namespace SmartTaskApp.ProjectManagement.WebApi.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> GetProjectByIdAsync(Guid id)
        {
            return await _projectRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllAsync();
        }

        public async Task CreateProjectAsync(Project project)
        {
            await _projectRepository.AddAsync(project);
        }

        public async Task UpdateProjectAsync(Project project)
        {
            await _projectRepository.UpdateAsync(project);
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            await _projectRepository.DeleteAsync(id);
        }
    }
}
