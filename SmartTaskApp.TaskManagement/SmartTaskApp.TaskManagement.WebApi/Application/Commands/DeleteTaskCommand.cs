using MediatR;

namespace SmartTaskApp.TaskManagement.WebApi.Application.Commands
{
    public class DeleteTaskCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
