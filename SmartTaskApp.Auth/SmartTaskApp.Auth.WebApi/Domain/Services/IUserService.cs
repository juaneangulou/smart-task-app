using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.Auth.WebApi.Domain.Services
{
    public interface IUserService
    {
        Task<Guid> RegisterUserAsync(string email, string password, string firstName, string lastName, DateTime dateOfBirth);
        Task<(string AccessToken, string RefreshToken)> AuthenticateAsync(string email, string password);
        Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
        Task<User> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string userId);
        Task<string> GeneratePasswordResetTokenAsync(string email);
    }
}
