using MediatR;

namespace SmartTaskApp.Notifications.WebApi.Application.Commands
{
    public class DeleteNotificationCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
