namespace SmartTaskApp.Auth.WebApi
{
    using Microsoft.EntityFrameworkCore;
    using Serilog;
    using SmartTaskApp.Auth.WebApi.Configurations;
    using SmartTaskApp.CommonDb;
    using SmartTaskApp.CommonDb.Configurations;
    using SmartTaskApp.CommonDb.Infraestructure.SeedData;
    using SmartTaskApp.CommonLib.Middlewares;
    using SmartTaskApp.CommonLib.Shared;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();

            var connectionString = Environment.GetEnvironmentVariable(AppConstants.SmartTaskDbConnection_Key);
            var jwtSecretKey = Environment.GetEnvironmentVariable(AppConstants.JwtSecret_key);

            builder.Services.AddSmartTaskAppDbConfigurations(connectionString);
            builder.Services.AddIdentityServerConfigurations(jwtSecretKey);

            builder.Services.AddDIConfigurations();
            builder.Services.AddValidationConfigurations();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatRConfigurations();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<SmartTaskAppDbContext>();
                    context.Database.Migrate();
                    await DefaultRolesInitializer.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");
                }
            }

            app.UseSerilogRequestLogging();
            app.UseMiddleware<SmartTaskAppExceptionHandlingMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI();

            //TODO: Disable  just for  docker container
            // app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
