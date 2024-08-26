using MediatR;

namespace SmartTaskApp.ProjectManagement.WebApi.Application.Commands
{
    public class DeleteProjectCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
