using MediatR;
using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.Notifications.WebApi.Application.Commands
{
    public class CreateNotificationCommand : IRequest<Guid>
    {
        public string Message { get; set; }
        public string Recipient { get; set; }
        public NotificationType Type { get; set; }
    }
}
