using MediatR;
using SmartTaskApp.ProjectManagement.WebApi.Application.DTOs;
using SmartTaskApp.ProjectManagement.WebApi.Application.Queries;
using SmartTaskApp.ProjectManagement.WebApi.Domain.Services;

namespace SmartTaskApp.ProjectManagement.WebApi.Application.Handlers
{
    public class ProjectQueryHandler :
        IRequestHandler<GetProjectByIdQuery, ProjectDto>,
        IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
    {
        private readonly IProjectService _projectService;

        public ProjectQueryHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectService.GetProjectByIdAsync(request.Id);
            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Status = project.Status,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Owner = project.Owner
            };
        }

        public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectService.GetAllProjectsAsync();
            var projectDtos = new List<ProjectDto>();

            foreach (var project in projects)
            {
                projectDtos.Add(new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    Status = project.Status,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Owner = project.Owner
                });
            }

            return projectDtos;
        }
    }
}
