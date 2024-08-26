using Microsoft.EntityFrameworkCore;
using SmartTaskApp.CommonDb;
using SmartTaskApp.CommonDb.Entities;
using SmartTaskApp.TaskManagement.WebApi.Domain.Repositories;

namespace SmartTaskApp.TaskManagement.WebApi.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly SmartTaskAppDbContext _context;

        public TaskRepository(SmartTaskAppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> GetByIdAsync(Guid id)
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.TaskItems.ToListAsync();
        }

        public async Task AddAsync(TaskItem taskItem)
        {
            await _context.TaskItems.AddAsync(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem taskItem)
        {
            _context.TaskItems.Update(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem != null)
            {
                _context.TaskItems.Remove(taskItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
