using SmartTaskApp.ProjectManagement.WebApi.Domain.Repositories;
using SmartTaskApp.ProjectManagement.WebApi.Domain.Services;
using SmartTaskApp.ProjectManagement.WebApi.Infrastructure.Repositories;
using SmartTaskApp.ProjectManagement.WebApi.Infrastructure.Services;

namespace SmartTaskApp.ProjectManagement.WebApi.Configurations
{
    public static class DIConfigurations
    {
        public static IServiceCollection AddDIConfigurations(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            return services;
        }
    }
}
