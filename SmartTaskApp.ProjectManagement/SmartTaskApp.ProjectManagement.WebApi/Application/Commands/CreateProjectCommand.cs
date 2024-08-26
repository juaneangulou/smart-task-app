using MediatR;
using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.ProjectManagement.WebApi.Application.Commands
{
    public class CreateProjectCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Owner { get; set; }
    }
}
