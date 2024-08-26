using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SmartTaskApp.CommonDb.Configurations
{
    public static class SmartTaskAppDbConfigurations
    {
        public static IServiceCollection AddSmartTaskAppDbConfigurations(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SmartTaskAppDbContext>(options =>
              options.UseNpgsql(connectionString));

            return services;
        }
    }
}
