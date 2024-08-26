using MediatR;
using SmartTaskApp.Notifications.WebApi.Application.DTOs;
using System;

namespace SmartTaskApp.Notifications.WebApi.Application.Queries
{
    public class GetNotificationByIdQuery : IRequest<NotificationDto>
    {
        public Guid Id { get; set; }
    }
}
