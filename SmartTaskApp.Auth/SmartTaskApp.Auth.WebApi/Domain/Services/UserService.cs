using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SmartTaskApp.Auth.WebApi.Domain.Repositories;
using SmartTaskApp.CommonDb.Entities;
using SmartTaskApp.CommonLib.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartTaskApp.Auth.WebApi.Domain.Services
{

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _configuration;

        public UserService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IRefreshTokenRepository refreshTokenRepository,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;
        }

        public async Task<Guid> RegisterUserAsync(string email, string password, string firstName, string lastName, DateTime dateOfBirth)
        {
            var user = new User
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, AppConstants.Roles.User);
                return Guid.Parse(user.Id);
            }
            throw new Exception(AppConstants.Messages.UserCreationFailed);
        }

        public async Task<(string AccessToken, string RefreshToken)> AuthenticateAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(user.Id);

            await _refreshTokenRepository.AddAsync(refreshToken);

            return (accessToken, refreshToken.Token);
        }

        public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken)
        {
            var existingToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (existingToken == null || existingToken.IsExpired)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            var user = await _userManager.FindByIdAsync(existingToken.UserId);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }

            var newAccessToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken(user.Id);

            await _refreshTokenRepository.RemoveAsync(existingToken);
            await _refreshTokenRepository.AddAsync(newRefreshToken);

            return (newAccessToken, newRefreshToken.Token);
        }

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(string userId)
        {
            return new RefreshToken(userId);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }
    }
}
