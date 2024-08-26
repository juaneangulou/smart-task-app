using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SmartTaskApp.CommonDb;
using SmartTaskApp.CommonDb.Entities;
using System.Text;

namespace SmartTaskApp.Auth.WebApi.Configurations
{
    public static class IdentityServerConfigurations
    {
        public static IServiceCollection AddIdentityServerConfigurations(this IServiceCollection services, string key)
        {
           
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
            })
                .AddEntityFrameworkStores<SmartTaskAppDbContext>()
                .AddDefaultTokenProviders();

            var jwtKey = Encoding.ASCII.GetBytes(key);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            return services;
        }
    }
}
