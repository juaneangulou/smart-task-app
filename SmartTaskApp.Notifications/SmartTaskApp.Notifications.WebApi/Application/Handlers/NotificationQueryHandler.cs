using MediatR;
using SmartTaskApp.Notifications.WebApi.Application.DTOs;
using SmartTaskApp.Notifications.WebApi.Application.Queries;
using SmartTaskApp.Notifications.WebApi.Domain.Services;

namespace SmartTaskApp.Notifications.WebApi.Application.Handlers
{
    public class NotificationQueryHandler :
        IRequestHandler<GetNotificationByIdQuery, NotificationDto>,
        IRequestHandler<GetAllNotificationsQuery, IEnumerable<NotificationDto>>
    {
        private readonly INotificationService _notificationService;

        public NotificationQueryHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<NotificationDto> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(request.Id);
            return new NotificationDto
            {
                Id = notification.Id,
                Message = notification.Message,
                Recipient = notification.Recipient,
                Type = notification.Type,
                SentAt = notification.SentAt,
                IsSent = notification.IsSent
            };
        }

        public async Task<IEnumerable<NotificationDto>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _notificationService.GetAllNotificationsAsync();
            var notificationDtos = new List<NotificationDto>();

            foreach (var notification in notifications)
            {
                notificationDtos.Add(new NotificationDto
                {
                    Id = notification.Id,
                    Message = notification.Message,
                    Recipient = notification.Recipient,
                    Type = notification.Type,
                    SentAt = notification.SentAt,
                    IsSent = notification.IsSent
                });
            }

            return notificationDtos;
        }
    }
}
