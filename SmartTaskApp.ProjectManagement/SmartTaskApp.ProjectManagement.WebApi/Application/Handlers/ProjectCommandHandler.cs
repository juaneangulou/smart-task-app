using MediatR;
using SmartTaskApp.CommonDb.Entities;
using SmartTaskApp.ProjectManagement.WebApi.Application.Commands;
using SmartTaskApp.ProjectManagement.WebApi.Domain.Services;

namespace SmartTaskApp.ProjectManagement.WebApi.Application.Handlers
{
    public class ProjectCommandHandler :
        IRequestHandler<CreateProjectCommand, Guid>,
        IRequestHandler<UpdateProjectCommand, bool>,
        IRequestHandler<DeleteProjectCommand, bool>
    {
        private readonly IProjectService _projectService;

        public ProjectCommandHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Status = request.Status,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Owner = request.Owner
            };

            await _projectService.CreateProjectAsync(project);
            return project.Id;
        }

        public async Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Status = request.Status,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Owner = request.Owner
            };

            await _projectService.UpdateProjectAsync(project);
            return true;
        }

        public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            await _projectService.DeleteProjectAsync(request.Id);
            return true;
        }
    }
}
