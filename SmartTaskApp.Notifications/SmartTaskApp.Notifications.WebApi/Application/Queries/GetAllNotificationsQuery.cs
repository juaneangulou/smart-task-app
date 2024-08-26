using MediatR;
using SmartTaskApp.Notifications.WebApi.Application.DTOs;

namespace SmartTaskApp.Notifications.WebApi.Application.Queries
{
    public class GetAllNotificationsQuery : IRequest<IEnumerable<NotificationDto>>
    {
    }
}
