namespace SmartTaskApp.ProjectManagement
{
    using Serilog;
    using SmartTaskApp.CommonLib.Configurations;
    using SmartTaskApp.CommonDb.Configurations;
    using SmartTaskApp.CommonLib.Shared;
    using SmartTaskApp.ProjectManagement.WebApi.Configurations;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                 ?? Environment.GetEnvironmentVariable(AppConstants.SmartTaskDbConnection_Key);
            var jwtSecretKey = builder.Configuration.GetValue<string>(AppConstants.JwtSecret_key)
                              ?? Environment.GetEnvironmentVariable(AppConstants.JwtSecret_key);

            builder.Services.AddSmartTaskAppDbConfigurations(connectionString);
            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.Services.AddDIConfigurations();
            builder.Services.AddMediatRConfigurations();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSmartTaskAppPipeline();
            app.MapControllers();

            app.Run();
        }
    }

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
}
