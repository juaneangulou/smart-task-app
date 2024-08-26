using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.Notifications.WebApi.Application.DTOs
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Recipient { get; set; }
        public NotificationType Type { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsSent { get; set; }
    }
}
