using Microsoft.EntityFrameworkCore;
using SmartTaskApp.CommonDb;
using SmartTaskApp.CommonDb.Entities;
using SmartTaskApp.ProjectManagement.WebApi.Domain.Repositories;

namespace SmartTaskApp.ProjectManagement.WebApi.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly SmartTaskAppDbContext _context;

        public ProjectRepository(SmartTaskAppDbContext context)
        {
            _context = context;
        }

        public async Task<Project> GetByIdAsync(Guid id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }
    }
}
