using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartTaskApp.CommonDb;
using SmartTaskApp.TaskManagement.WebApi;
using SmartTaskApp.Auth.IntegrationTests;

namespace SmartTaskApp.TaskManagement.IntegrationTests
{
    public class TaskManagementIntegrationTestBase : IClassFixture<WebApplicationFactory<Startup>>
    {
        public readonly HttpClient TaskClient;
        private readonly WebApplicationFactory<Startup> _taskFactory;
        private readonly AuthIntegrationTestBase _authIntegrationTestBase;

        public TaskManagementIntegrationTestBase(WebApplicationFactory<Startup> factory, AuthIntegrationTestBase authIntegrationTestBase)
        {
            _taskFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddEnvironmentVariables();
                });

                builder.ConfigureServices(async services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<SmartTaskAppDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<SmartTaskAppDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });

                    var sp = services.BuildServiceProvider();
                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<SmartTaskAppDbContext>();

                        db.Database.EnsureCreated();
                    }
                });
            });

            _authIntegrationTestBase = authIntegrationTestBase;
            TaskClient = _taskFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            var token = await _authIntegrationTestBase.GetAuthTokenAsync();
            TaskClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}
