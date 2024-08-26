using MediatR;
using SmartTaskApp.CommonDb.Entities;
using SmartTaskApp.TaskManagement.WebApi.Application.Commands;
using SmartTaskApp.TaskManagement.WebApi.Domain.Services;

namespace SmartTaskApp.TaskManagement.WebApi.Application.Handlers
{
    public class TaskCommandHandler :
        IRequestHandler<CreateTaskCommand, Guid>,
        IRequestHandler<UpdateTaskCommand, bool>,
        IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly ITaskService _taskService;

        public TaskCommandHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Priority = request.Priority,
                DueDate = request.DueDate,
                AssignedTo = request.AssignedTo
            };

            await _taskService.CreateTaskAsync(task);
            return task.Id;
        }

        public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TaskItem
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Priority = request.Priority,
                DueDate = request.DueDate,
                AssignedTo = request.AssignedTo
            };

            await _taskService.UpdateTaskAsync(task);
            return true;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            await _taskService.DeleteTaskAsync(request.Id);
            return true;
        }
    }
}
