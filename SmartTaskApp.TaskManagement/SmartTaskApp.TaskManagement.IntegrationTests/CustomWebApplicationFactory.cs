using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace SmartTaskApp.TaskManagement.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(static services =>
            {
                var authServiceProvider = new ServiceCollection()
                    .AddSingleton(static sp => new AuthWebApplicationFactory<SmartTaskApp.Auth.WebApi.Program>())
                    .BuildServiceProvider();
            });
        }
    }

    public class AuthWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseStartup<TStartup>();
        }
    }
}
