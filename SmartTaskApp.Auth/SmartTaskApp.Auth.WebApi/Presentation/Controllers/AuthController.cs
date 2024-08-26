using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTaskApp.Auth.WebApi.Application.Commands.DeleteUser;
using SmartTaskApp.Auth.WebApi.Application.Commands.LoginUser;
using SmartTaskApp.Auth.WebApi.Application.Commands.MakeAdmin;
using SmartTaskApp.Auth.WebApi.Application.Commands.RegisterUser;
using SmartTaskApp.Auth.WebApi.Application.Commands.ResetPassword;
using SmartTaskApp.CommonLib.Shared;


namespace SmartTaskApp.Auth.WebApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var userId = await _mediator.Send(command);
            return Ok(new { UserId = userId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var tokens = await _mediator.Send(command);
            return Ok(new { AccessToken = tokens.accessToken, RefreshToken = tokens.refreshToken });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return result ? Ok("Password reset successfully.") : BadRequest("Failed to reset password.");
        }

        [Authorize(Roles = AppConstants.AdminRole)]
        [HttpPost("make-admin")]
        public async Task<IActionResult> MakeAdmin([FromBody] MakeAdminCommand command)
        {
            var result = await _mediator.Send(command);
            return result ? Ok() : BadRequest("Failed to make user admin.");
        }

        [Authorize(Roles = AppConstants.AdminRole)]
        [HttpDelete("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var command = new DeleteUserCommand { UserId = userId };
            var result = await _mediator.Send(command);
            return result ? Ok() : BadRequest("Failed to delete user.");
        }
    }
}