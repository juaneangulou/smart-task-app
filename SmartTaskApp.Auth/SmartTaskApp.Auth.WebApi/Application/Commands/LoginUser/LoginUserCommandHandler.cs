using MediatR;
using SmartTaskApp.Auth.WebApi.Domain.Services;

namespace SmartTaskApp.Auth.WebApi.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, (string accessToken, string refreshToken)>
    {
        private readonly IUserService _userService;

        public LoginUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<(string accessToken, string refreshToken)> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.AuthenticateAsync(request.Email, request.Password);
        }
    }
}

