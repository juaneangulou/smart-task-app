using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.TaskManagement.WebApi.Domain.Services
{
    public interface ITaskService
    {
        Task<TaskItem> GetTaskByIdAsync(Guid id);
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task CreateTaskAsync(TaskItem taskItem);
        Task UpdateTaskAsync(TaskItem taskItem);
        Task DeleteTaskAsync(Guid id);
    }
}
