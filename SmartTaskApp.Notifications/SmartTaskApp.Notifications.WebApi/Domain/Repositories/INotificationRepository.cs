using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.Notifications.WebApi.Domain.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> GetByIdAsync(Guid id);
        Task<IEnumerable<Notification>> GetAllAsync();
        Task AddAsync(Notification notification);
        Task UpdateAsync(Notification notification);
        Task DeleteAsync(Guid id);
    }
}
