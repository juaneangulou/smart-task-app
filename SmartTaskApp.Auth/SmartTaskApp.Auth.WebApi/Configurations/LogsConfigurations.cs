using Serilog;

namespace SmartTaskApp.Auth.WebApi.Configurations
{
    public static  class LogsConfigurations
    {
        public static IServiceCollection AddLogsConfigurations(this IServiceCollection services)
        {
            services.AddLogging(cfg => cfg.AddSerilog());
            return services;
        }
    }
}
