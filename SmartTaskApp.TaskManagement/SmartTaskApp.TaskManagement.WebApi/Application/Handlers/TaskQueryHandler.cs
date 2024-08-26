using MediatR;
using SmartTaskApp.TaskManagement.WebApi.Application.DTOs;
using SmartTaskApp.TaskManagement.WebApi.Application.Queries;
using SmartTaskApp.TaskManagement.WebApi.Domain.Services;

namespace SmartTaskApp.TaskManagement.WebApi.Application.Handlers
{
    public class TaskQueryHandler :
        IRequestHandler<GetTaskByIdQuery, TaskItemDto>,
        IRequestHandler<GetAllTasksQuery, IEnumerable<TaskItemDto>>
    {
        private readonly ITaskService _taskService;

        public TaskQueryHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<TaskItemDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var taskItem = await _taskService.GetTaskByIdAsync(request.Id);
            return new TaskItemDto
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                Status = taskItem.Status,
                Priority = taskItem.Priority,
                DueDate = taskItem.DueDate,
                AssignedTo = taskItem.AssignedTo
            };
        }

        public async Task<IEnumerable<TaskItemDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var taskItems = await _taskService.GetAllTasksAsync();
            var taskItemDtos = new List<TaskItemDto>();

            foreach (var taskItem in taskItems)
            {
                taskItemDtos.Add(new TaskItemDto
                {
                    Id = taskItem.Id,
                    Title = taskItem.Title,
                    Description = taskItem.Description,
                    Status = taskItem.Status,
                    Priority = taskItem.Priority,
                    DueDate = taskItem.DueDate,
                    AssignedTo = taskItem.AssignedTo
                });
            }

            return taskItemDtos;
        }
    }
}
