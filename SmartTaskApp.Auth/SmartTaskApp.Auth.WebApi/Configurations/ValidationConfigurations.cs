using FluentValidation.AspNetCore;
using SmartTaskApp.Auth.WebApi.Validations;

namespace SmartTaskApp.Auth.WebApi.Configurations
{
    public static class ValidationConfigurations
    {
        public static IServiceCollection AddValidationConfigurations(this IServiceCollection services)
        {
           services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterUserCommandValidator>());
            return services;
        }
    }
}
