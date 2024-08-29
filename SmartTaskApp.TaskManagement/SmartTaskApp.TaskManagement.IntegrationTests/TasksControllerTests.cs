//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using SmartTaskApp.Auth.WebApi;
//using SmartTaskApp.CommonDb;
//using SmartTaskApp.CommonDb.Infraestructure.SeedData;
//using SmartTaskApp.TaskManagement.WebApi;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Xunit;
//using FluentAssertions;
//using System.Net.Http.Json;
//using ProgramAuth = SmartTaskApp.Auth.WebApi.Program;

//namespace SmartTaskApp.Auth.IntegrationTests
//{
//    public class AuthIntegrationTestBase : IClassFixture<WebApplicationFactory<ProgramAuth>>
//    {
//        public readonly HttpClient AuthClient;
//        private readonly WebApplicationFactory<ProgramAuth> _factory;

//        public AuthIntegrationTestBase(WebApplicationFactory<ProgramAuth> factory)
//        {
//            _factory = factory.WithWebHostBuilder(builder =>
//            {
//                builder.ConfigureServices(async services =>
//                {
//                    var descriptor = services.SingleOrDefault(
//                        d => d.ServiceType == typeof(DbContextOptions<SmartTaskAppDbContext>));
//                    if (descriptor != null)
//                    {
//                        services.Remove(descriptor);
//                    }

//                    services.AddDbContext<SmartTaskAppDbContext>(options =>
//                    {
//                        options.UseInMemoryDatabase("TestDb");
//                    });

//                    var sp = services.BuildServiceProvider();
//                    using (var scope = sp.CreateScope())
//                    {
//                        var scopedServices = scope.ServiceProvider;
//                        var db = scopedServices.GetRequiredService<SmartTaskAppDbContext>();
//                        await DefaultRolesInitializer.Initialize(scopedServices);

//                        db.Database.EnsureCreated();
//                    }
//                });
//            });

//            AuthClient = _factory.CreateClient();
//        }

//        public async Task<string> GetAuthTokenAsync()
//        {
//            var loginRequest = new
//            {
//                Username = "testuser",
//                Password = "Test@123"
//            };

//            var response = await AuthClient.PostAsJsonAsync("/api/auth/login", loginRequest);
//            response.EnsureSuccessStatusCode();

//            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
//            return authResponse.Token;
//        }

//        public class AuthResponse
//        {
//            public string Token { get; set; }
//        }
//    }

//    public class TaskManagementIntegrationTestBase : IClassFixture<WebApplicationFactory<Startup>>
//    {
//        public readonly HttpClient TaskClient;
//        public readonly AuthIntegrationTestBase _authTestBase;

//        public TaskManagementIntegrationTestBase(WebApplicationFactory<Startup> factory, AuthIntegrationTestBase authTestBase)
//        {
//            _authTestBase = authTestBase;

//            factory = factory.WithWebHostBuilder(builder =>
//            {
//                builder.ConfigureServices(services =>
//                {
//                    var descriptor = services.SingleOrDefault(
//                        d => d.ServiceType == typeof(DbContextOptions<SmartTaskAppDbContext>));
//                    if (descriptor != null)
//                    {
//                        services.Remove(descriptor);
//                    }

//                    services.AddDbContext<SmartTaskAppDbContext>(options =>
//                    {
//                        options.UseInMemoryDatabase("TaskTestDb");
//                    });

//                    var sp = services.BuildServiceProvider();
//                    using (var scope = sp.CreateScope())
//                    {
//                        var scopedServices = scope.ServiceProvider;
//                        var db = scopedServices.GetRequiredService<SmartTaskAppDbContext>();

//                        db.Database.EnsureCreated();
//                    }
//                });
//            });

//            TaskClient = factory.CreateClient();
//        }
//    }

//    public class TasksControllerTests : IClassFixture<TaskManagementIntegrationTestBase>
//    {
//        private readonly TaskManagementIntegrationTestBase _taskManagementTestBase;

//        public TasksControllerTests(TaskManagementIntegrationTestBase taskManagementTestBase)
//        {
//            _taskManagementTestBase = taskManagementTestBase;
//        }

//        [Fact]
//        public async Task GetTasks_ReturnsOk_WhenUserIsAuthenticated()
//        {
//            var token = await _taskManagementTestBase._authTestBase.GetAuthTokenAsync();
//            _taskManagementTestBase.TaskClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

//            var response = await _taskManagementTestBase.TaskClient.GetAsync("/api/tasks");
//            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
//        }

//        [Fact]
//        public async Task GetTasks_ReturnsUnauthorized_WhenUserIsNotAuthenticated()
//        {
//            _taskManagementTestBase.TaskClient.DefaultRequestHeaders.Authorization = null;

//            var response = await _taskManagementTestBase.TaskClient.GetAsync("/api/tasks");

//            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
//        }
//    }
//}
