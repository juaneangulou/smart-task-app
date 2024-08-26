using SmartTaskApp.TaskManagement.WebApi.Domain.Repositories;
using SmartTaskApp.TaskManagement.WebApi.Domain.Services;
using SmartTaskApp.TaskManagement.WebApi.Infrastructure.Repositories;
using SmartTaskApp.TaskManagement.WebApi.Infrastructure.Services;

namespace SmartTaskApp.TaskManagement.WebApi.Configurations
{
    public static class DIConfigurations
    {
        public static IServiceCollection AddDIConfigurations(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskItemService>();
            services.AddScoped<ITaskRepository, TaskRepository>();

            return services;
        }
    }
}
