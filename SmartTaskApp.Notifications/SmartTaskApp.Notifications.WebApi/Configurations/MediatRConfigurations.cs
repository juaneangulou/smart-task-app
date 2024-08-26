namespace SmartTaskApp.Notifications.WebApi.Configurations
{
    public static class MediatRConfigurations
    {
        public static IServiceCollection AddMediatRConfigurations(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Notifications.WebApi.Configurations.MediatRConfigurations).Assembly));
            return services;
        }
    }
}
