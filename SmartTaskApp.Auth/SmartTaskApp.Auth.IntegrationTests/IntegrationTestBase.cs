﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartTaskApp.Auth.WebApi;
using SmartTaskApp.Auth.WebApi.Configurations;
using SmartTaskApp.CommonDb;
using SmartTaskApp.CommonDb.Entities;
using SmartTaskApp.CommonDb.Infraestructure.SeedData;
using SmartTaskApp.CommonLib.Shared;

namespace SmartTaskApp.Auth.IntegrationTests
{
    public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient TestClient;
        private readonly WebApplicationFactory<Program> _factory;

        public IntegrationTestBase(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
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

            TestClient = _factory.CreateClient();
        }
    }
}
