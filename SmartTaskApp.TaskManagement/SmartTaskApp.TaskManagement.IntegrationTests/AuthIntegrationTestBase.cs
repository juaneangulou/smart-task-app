using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartTaskApp.Auth.WebApi;
using SmartTaskApp.CommonDb;
using SmartTaskApp.CommonDb.Infraestructure.SeedData;
using System.Net.Http.Json;

namespace SmartTaskApp.Auth.IntegrationTests
{
    public class AuthIntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>
    {
        public readonly HttpClient AuthClient;
        private readonly WebApplicationFactory<Program> _authFactory;

        public AuthIntegrationTestBase(WebApplicationFactory<Program> factory)
        {
            _authFactory = factory.WithWebHostBuilder(builder =>
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
                        await DefaultRolesInitializer.Initialize(scopedServices);

                        db.Database.EnsureCreated();
                    }
                });
            });

            AuthClient = _authFactory.CreateClient();
        }

        public async Task<string> GetAuthTokenAsync()
        {
            var loginRequest = new
            {
                Username = "testuser",
                Password = "Test@123"
            };

            var response = await AuthClient.PostAsJsonAsync("/api/auth/login", loginRequest);
            response.EnsureSuccessStatusCode();

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
            return authResponse.Token;
        }

        public class AuthResponse
        {
            public string Token { get; set; }
        }
    }
}
