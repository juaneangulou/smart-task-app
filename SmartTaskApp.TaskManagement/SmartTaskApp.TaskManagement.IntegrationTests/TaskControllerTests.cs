using Microsoft.AspNetCore.Mvc.Testing;
using SmartTaskApp.CommonDb.Entities;
using SmartTaskApp.TaskManagement.WebApi.Application.Commands;
using System.Net;
using System.Net.Http.Json;
using TaskStatus = SmartTaskApp.CommonDb.Entities.TaskStatus;

namespace SmartTaskApp.TaskManagement.IntegrationTests
{
    public class TaskControllerTests : IntegrationTestBase
    {
        public TaskControllerTests(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task CreateTask_Should_Return_Ok_When_Task_Is_Valid()
        {
            var createTaskCommand = new CreateTaskCommand
            {
                Title = "New Task",
                Description = "Task Description",
                Priority = TaskPriority.Medium,
                Status = TaskStatus.Pending
            };

            var response = await TestClient.PostAsJsonAsync("/api/tasks", createTaskCommand);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetTaskById_Should_Return_Task()
        {
            var taskId = Guid.NewGuid();
            var createTaskCommand = new CreateTaskCommand
            {
                Title = "New Task",
                Description = "Task Description",
                Priority = TaskPriority.Medium,
                Status = TaskStatus.Pending
            };

            await TestClient.PostAsJsonAsync("/api/tasks", createTaskCommand);

            var response = await TestClient.GetAsync($"/api/tasks/{taskId}");
            response.EnsureSuccessStatusCode();
            var task = await response.Content.ReadFromJsonAsync<TaskItem>();

            Assert.NotNull(task);
            Assert.Equal("New Task", task.Title);
        }

        [Fact]
        public async Task UpdateTask_Should_Return_Ok_When_Task_Is_Updated()
        {
            var taskId = Guid.NewGuid();
            var createTaskCommand = new CreateTaskCommand
            {
                Title = "New Task",
                Description = "Task Description",
                Priority = TaskPriority.Medium,
                Status = TaskStatus.Pending
            };

            await TestClient.PostAsJsonAsync("/api/tasks", createTaskCommand);

            var updateTaskCommand = new UpdateTaskCommand
            {
                Id = taskId,
                Title = "Updated Task",
                Description = "Updated Task Description",
                Priority = TaskPriority.High,
                Status = TaskStatus.InProgress
            };

            var response = await TestClient.PutAsJsonAsync($"/api/tasks/{taskId}", updateTaskCommand);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteTask_Should_Return_Ok_When_Task_Is_Deleted()
        {
            var taskId = Guid.NewGuid();
            var createTaskCommand = new CreateTaskCommand
            {
                Title = "New Task",
                Description = "Task Description",
                Priority = TaskPriority.Medium,
                Status = TaskStatus.Pending
            };

            await TestClient.PostAsJsonAsync("/api/tasks", createTaskCommand);

            var response = await TestClient.DeleteAsync($"/api/tasks/{taskId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
