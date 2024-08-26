using Moq;
using SmartTaskApp.CommonDb.Entities;
using SmartTaskApp.TaskManagement.WebApi.Application.Commands;
using SmartTaskApp.TaskManagement.WebApi.Application.Handlers;
using SmartTaskApp.TaskManagement.WebApi.Domain.Services;
using TaskStatus = SmartTaskApp.CommonDb.Entities.TaskStatus;

namespace SmartTaskApp.TaskManagement.UnitTests.Commands
{
    public class TaskCommandHandlerTests
    {
        private readonly Mock<ITaskService> _taskServiceMock;
        private readonly TaskCommandHandler _handler;

        public TaskCommandHandlerTests()
        {
            _taskServiceMock = new Mock<ITaskService>();
            _handler = new TaskCommandHandler(_taskServiceMock.Object);
        }

        [Fact]
        public async Task Handle_CreateTask_Should_Create_Task()
        {
            var command = new CreateTaskCommand
            {
                Title = "New Task",
                Description = "Task Description",
                Priority = TaskPriority.Medium,
                Status = TaskStatus.Pending
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            _taskServiceMock.Verify(service => service.CreateTaskAsync(It.IsAny<TaskItem>()), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Handle_UpdateTask_Should_Update_Task()
        {
            var taskId = Guid.NewGuid();
            var task = new TaskItem { Id = taskId, Title = "Old Task", Description = "Old Description" };
            _taskServiceMock.Setup(service => service.GetTaskByIdAsync(It.IsAny<Guid>())).ReturnsAsync(task);

            var command = new UpdateTaskCommand
            {
                Id = task.Id,
                Title = "Updated Task",
                Description = "Updated Description",
                Priority = TaskPriority.High,
                Status = TaskStatus.InProgress
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            _taskServiceMock.Verify(service => service.UpdateTaskAsync(It.IsAny<TaskItem>()), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_DeleteTask_Should_Delete_Task()
        {
            var taskId = Guid.NewGuid();
            var task = new TaskItem { Id = taskId, Title = "Task to Delete", Description = "Description" };
            _taskServiceMock.Setup(service => service.GetTaskByIdAsync(taskId)).ReturnsAsync(task);

            var command = new DeleteTaskCommand { Id = taskId };

            var result = await _handler.Handle(command, CancellationToken.None);

            _taskServiceMock.Verify(service => service.DeleteTaskAsync(It.IsAny<Guid>()), Times.Once);
            Assert.True(result);
        }
    }
}
