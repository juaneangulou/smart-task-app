using SmartTaskApp.Auth.WebApi.Application.Commands.LoginUser;
using SmartTaskApp.Auth.WebApi.Application.Commands.RegisterUser;

namespace SmartTaskApp.Auth.WebApi.Configurations
{
    public static class MediatRConfigurations
    {
        public static IServiceCollection AddMediatRConfigurations(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginUserCommand).Assembly));
            return services;
        }
    }
}
