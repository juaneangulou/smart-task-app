using MediatR;
using SmartTaskApp.TaskManagement.WebApi.Application.DTOs;

namespace SmartTaskApp.TaskManagement.WebApi.Application.Queries
{
    public class GetAllTasksQuery : IRequest<IEnumerable<TaskItemDto>>
    {
    }
}
