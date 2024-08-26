using SmartTaskApp.CommonDb.Entities;
using SmartTaskApp.TaskManagement.WebApi.Domain.Repositories;
using SmartTaskApp.TaskManagement.WebApi.Domain.Services;

namespace SmartTaskApp.TaskManagement.WebApi.Infrastructure.Services
{
    public class TaskItemService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskItemService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskItem> GetTaskByIdAsync(Guid id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task CreateTaskAsync(TaskItem taskItem)
        {
            await _taskRepository.AddAsync(taskItem);
        }

        public async Task UpdateTaskAsync(TaskItem taskItem)
        {
            await _taskRepository.UpdateAsync(taskItem);
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            await _taskRepository.DeleteAsync(id);
        }
    }
}
