using MediatR;
using SmartTaskApp.CommonDb.Entities;
using SmartTaskApp.Notifications.WebApi.Application.Commands;
using SmartTaskApp.Notifications.WebApi.Domain.Services;

namespace SmartTaskApp.Notifications.WebApi.Application.Handlers
{
    public class NotificationCommandHandler :
        IRequestHandler<CreateNotificationCommand, Guid>,
        IRequestHandler<UpdateNotificationCommand, bool>,
        IRequestHandler<DeleteNotificationCommand, bool>
    {
        private readonly INotificationService _notificationService;

        public NotificationCommandHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<Guid> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                Message = request.Message,
                Recipient = request.Recipient,
                Type = request.Type
            };

            await _notificationService.CreateNotificationAsync(notification);
            return notification.Id;
        }

        public async Task<bool> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                Id = request.Id,
                Message = request.Message,
                Recipient = request.Recipient,
                Type = request.Type
            };

            await _notificationService.UpdateNotificationAsync(notification);
            return true;
        }

        public async Task<bool> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            await _notificationService.DeleteNotificationAsync(request.Id);
            return true;
        }
    }
}
