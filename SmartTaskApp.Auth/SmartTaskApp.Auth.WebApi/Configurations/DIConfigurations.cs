using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SmartTaskApp.Auth.WebApi.Application.Commands.RegisterUser;
using SmartTaskApp.Auth.WebApi.Domain.Repositories;
using SmartTaskApp.Auth.WebApi.Domain.Services;
using SmartTaskApp.Auth.WebApi.Infraestructure.Data;
using SmartTaskApp.Auth.WebApi.Validations;
using SmartTaskApp.CommonDb.Entities;

namespace  SmartTaskApp.Auth.WebApi.Configurations
{
    public static class DIConfigurations
    {
        public static IServiceCollection AddDIConfigurations(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>>();
            services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();

            return services;
        }
    }
}
