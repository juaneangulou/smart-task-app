using MediatR;
using SmartTaskApp.ProjectManagement.WebApi.Application.DTOs;

namespace SmartTaskApp.ProjectManagement.WebApi.Application.Queries
{
    public class GetProjectByIdQuery : IRequest<ProjectDto>
    {
        public Guid Id { get; set; }
    }
}
