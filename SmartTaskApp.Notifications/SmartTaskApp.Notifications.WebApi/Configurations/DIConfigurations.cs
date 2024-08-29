using SmartTaskApp.Notifications.WebApi.Domain.Repositories;
using SmartTaskApp.Notifications.WebApi.Domain.Services;
using SmartTaskApp.Notifications.WebApi.Infrastructure.Repositories;
using SmartTaskApp.Notifications.WebApi.Infrastructure.Services;

namespace SmartTaskApp.Notifications.WebApi.Configurations
{
    public static class DIConfigurations
    {
        public static IServiceCollection AddDIConfigurations(this IServiceCollection services)
        {
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();


            return services;
        }
    }
}
