using SmartTaskApp.CommonDb.Entities;
using SmartTaskApp.Notifications.WebApi.Domain.Repositories;
using SmartTaskApp.Notifications.WebApi.Domain.Services;

namespace SmartTaskApp.Notifications.WebApi.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Notification> GetNotificationByIdAsync(Guid id)
        {
            return await _notificationRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            return await _notificationRepository.GetAllAsync();
        }

        public async Task CreateNotificationAsync(Notification notification)
        {
            await _notificationRepository.AddAsync(notification);
        }

        public async Task UpdateNotificationAsync(Notification notification)
        {
            await _notificationRepository.UpdateAsync(notification);
        }

        public async Task DeleteNotificationAsync(Guid id)
        {
            await _notificationRepository.DeleteAsync(id);
        }
    }
}
