using Microsoft.EntityFrameworkCore;
using SmartTaskApp.CommonDb;
using SmartTaskApp.CommonDb.Entities;
using SmartTaskApp.Notifications.WebApi.Domain.Repositories;

namespace SmartTaskApp.Notifications.WebApi.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly SmartTaskAppDbContext _context;

        public NotificationRepository(SmartTaskAppDbContext context)
        {
            _context = context;
        }

        public async Task<Notification> GetByIdAsync(Guid id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task AddAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Notification notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }
    }
}
