using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.Notifications.WebApi.Domain.Services
{
    public interface INotificationService
    {
        Task<Notification> GetNotificationByIdAsync(Guid id);
        Task<IEnumerable<Notification>> GetAllNotificationsAsync();
        Task CreateNotificationAsync(Notification notification);
        Task UpdateNotificationAsync(Notification notification);
        Task DeleteNotificationAsync(Guid id);
    }
}
