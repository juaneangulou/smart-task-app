using Serilog;
using SmartTaskApp.CommonLib.Configurations;
using SmartTaskApp.CommonLib.Shared;
using SmartTaskApp.CommonDb.Configurations;
using SmartTaskApp.TaskManagement.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable(AppConstants.SmartTaskDbConnection_Key);
var jwtSecretKey = builder.Configuration.GetValue<string>(AppConstants.JwtSecret_key)
                  ?? Environment.GetEnvironmentVariable(AppConstants.JwtSecret_key);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
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

public partial class Program { }
