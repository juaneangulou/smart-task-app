using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.TaskManagement.WebApi.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskItem> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task AddAsync(TaskItem taskItem);
        Task UpdateAsync(TaskItem taskItem);
        Task DeleteAsync(Guid id);
    }
}
