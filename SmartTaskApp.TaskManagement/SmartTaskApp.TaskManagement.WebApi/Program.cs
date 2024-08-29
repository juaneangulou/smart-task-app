using Serilog;
using SmartTaskApp.CommonLib.Configurations;
using SmartTaskApp.CommonLib.Shared;
using SmartTaskApp.CommonDb.Configurations;
using SmartTaskApp.TaskManagement.WebApi.Configurations;
namespace SmartTaskApp.TaskManagement.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection")
                                 ?? Environment.GetEnvironmentVariable(AppConstants.SmartTaskDbConnection_Key);
            var jwtSecretKey = _configuration.GetValue<string>(AppConstants.JwtSecret_key)
                              ?? Environment.GetEnvironmentVariable(AppConstants.JwtSecret_key);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddSmartTaskAppDbConfigurations(connectionString);
            services.AddJwtAuthentication(_configuration);
            services.AddDIConfigurations();
            services.AddMediatRConfigurations();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(WebApplication app)
        {
            app.UseSmartTaskAppPipeline();
            app.MapControllers();
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            var startup = new Startup(builder.Configuration);

            startup.ConfigureServices(builder.Services);

            var app = builder.Build();

            startup.Configure(app);

            app.Run();
        }
    }
}
