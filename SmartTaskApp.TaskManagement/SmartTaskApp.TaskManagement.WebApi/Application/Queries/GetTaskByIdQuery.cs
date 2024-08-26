using MediatR;
using SmartTaskApp.TaskManagement.WebApi.Application.DTOs;

namespace SmartTaskApp.TaskManagement.WebApi.Application.Queries
{
    public class GetTaskByIdQuery : IRequest<TaskItemDto>
    {
        public Guid Id { get; set; }
    }
}
