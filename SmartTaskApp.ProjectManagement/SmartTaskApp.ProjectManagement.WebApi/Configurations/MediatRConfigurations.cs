namespace SmartTaskApp.ProjectManagement.WebApi.Configurations
{

    public static class MediatRConfigurations
    {
        public static IServiceCollection AddMediatRConfigurations(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProjectManagement.WebApi.Configurations.MediatRConfigurations).Assembly));
            return services;
        }
    }
}
