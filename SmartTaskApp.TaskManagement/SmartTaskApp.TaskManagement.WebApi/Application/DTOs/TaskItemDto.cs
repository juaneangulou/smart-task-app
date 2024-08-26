using SmartTaskApp.CommonDb.Entities;
using TaskStatus = SmartTaskApp.CommonDb.Entities.TaskStatus;

namespace SmartTaskApp.TaskManagement.WebApi.Application.DTOs
{
    public class TaskItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime DueDate { get; set; }
        public string AssignedTo { get; set; }
    }
}
