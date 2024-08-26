using MediatR;
using SmartTaskApp.ProjectManagement.WebApi.Application.DTOs;

namespace SmartTaskApp.ProjectManagement.WebApi.Application.Queries
{
    public class GetAllProjectsQuery : IRequest<IEnumerable<ProjectDto>>
    {
    }
}
