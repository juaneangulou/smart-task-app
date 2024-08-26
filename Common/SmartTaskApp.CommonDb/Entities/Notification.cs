namespace SmartTaskApp.CommonDb.Entities
{
    public enum NotificationType
    {
        Email,
        SMS,
        PushNotification
    }

    public class Notification
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Recipient { get; set; }
        public NotificationType Type { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsSent { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
