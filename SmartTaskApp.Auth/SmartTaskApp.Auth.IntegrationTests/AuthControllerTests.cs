using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace SmartTaskApp.Auth.IntegrationTests
{
    public class AuthControllerTests : IntegrationTestBase
    {
        public AuthControllerTests(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Register_Should_Return_Ok_When_User_Is_Valid()
        {
            var registerUserCommand = new
            {
                Email = "testuser1@example.com",
                Password = "Password123!",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = "1990-01-01"
            };

            var response = await TestClient.PostAsJsonAsync("/api/auth/register", registerUserCommand);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Register_Should_Return_BadRequest_When_Email_Is_Invalid()
        {
            var registerUserCommand = new
            {
                Email = "invalid-email",
                Password = "Password123!",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = "1990-01-01"
            };

            var response = await TestClient.PostAsJsonAsync("/api/auth/register", registerUserCommand);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Login_Should_Return_Token_When_Credentials_Are_Valid()
        {
            var registerUserCommand = new
            {
                Email = "testuser@example.com",
                Password = "Password123!",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = "1990-01-01"
            };

            await TestClient.PostAsJsonAsync("/api/auth/register", registerUserCommand);

            var loginCommand = new
            {
                Email = "testuser@example.com",
                Password = "Password123!"
            };

            var response = await TestClient.PostAsJsonAsync("/api/auth/login", loginCommand);

            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrEmpty(token));
        }

        [Fact]
        public async Task Login_Should_Return_Unauthorized_When_Credentials_Are_Invalid()
        {
            var loginCommand = new
            {
                Email = "nonexistentuser@example.com",
                Password = "WrongPassword!"
            };

            var response = await TestClient.PostAsJsonAsync("/api/auth/login", loginCommand);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}