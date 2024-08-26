using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using SmartTaskApp.Auth.WebApi.Domain.Repositories;
using SmartTaskApp.Auth.WebApi.Domain.Services;
using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.Auth.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<SignInManager<User>> _signInManagerMock;
        private readonly Mock<IRefreshTokenRepository> _refreshTokenRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        public UserServiceTests()
        {
            _userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

            _signInManagerMock = new Mock<SignInManager<User>>(
                _userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null);

            _refreshTokenRepositoryMock = new Mock<IRefreshTokenRepository>();
            _configurationMock = new Mock<IConfiguration>();

            _userService = new UserService(
                _userManagerMock.Object,
                _signInManagerMock.Object,
                _refreshTokenRepositoryMock.Object,
                _configurationMock.Object);
        }

        [Fact]
        public async Task RegisterUserAsync_Should_Return_Guid_When_Successful()
        {
            var user = new User { Id = Guid.NewGuid().ToString(), Email = "test@example.com" };
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _userService.RegisterUserAsync("test@example.com", "password", "Test", "User", DateTime.UtcNow);

            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task AuthenticateAsync_Should_Throw_Exception_When_User_Not_Found()
        {
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _userService.AuthenticateAsync("nonexistent@example.com", "password"));
        }

        [Fact]
        public void GenerateJwtToken_Should_Return_Token_When_User_Is_Valid()
        {
            var user = new User { Id = Guid.NewGuid().ToString(), UserName = "testuser", Email = "test@example.com" };
            _configurationMock.Setup(x => x["Jwt:Key"]).Returns("0000000000000000000000000000000000000000000");
            _configurationMock.Setup(x => x["Jwt:Issuer"]).Returns("SmartTaskApp");
            _configurationMock.Setup(x => x["Jwt:Audience"]).Returns("SmartTaskAppUsers");

            var token = _userService.GenerateJwtToken(user);

            Assert.NotNull(token);
        }

        [Fact]
        public async Task RegisterUserAsync_Should_ThrowArgumentNullException_When_EmailIsNull()
        {
            string email = null;
            var password = "Password123!";
            var firstName = "John";
            var lastName = "Doe";
            var dateOfBirth = DateTime.Now;

            await Assert.ThrowsAsync<NullReferenceException>(() => _userService.RegisterUserAsync(email, password, firstName, lastName, dateOfBirth));
        }

        [Fact]
        public async Task RegisterUserAsync_Should_Call_CreateAsync_Once()
        {
            var email = "test@example.com";
            var password = "Password123!";
            var firstName = "John";
            var lastName = "Doe";
            var dateOfBirth = DateTime.Now;

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Success);

            await _userService.RegisterUserAsync(email, password, firstName, lastName, dateOfBirth);

            _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<User>(), password), Times.Once);
        }

        [Fact]
        public async Task RegisterUserAsync_Should_ThrowException_When_CreateFails()
        {
            var email = "test@example.com";
            var password = "Password123!";
            var firstName = "John";
            var lastName = "Doe";
            var dateOfBirth = DateTime.Now;

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error creating user" }));

            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.RegisterUserAsync(email, password, firstName, lastName, dateOfBirth));
            Assert.Equal("Error creating user", exception.Message);
        }
    }
}