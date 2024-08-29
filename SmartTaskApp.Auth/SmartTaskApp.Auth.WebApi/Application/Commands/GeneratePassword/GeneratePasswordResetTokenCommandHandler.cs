using MediatR;
using SmartTaskApp.Auth.WebApi.Domain.Services;

namespace SmartTaskApp.Auth.WebApi.Application.Commands.GeneratePassword
{
    public class GeneratePasswordResetTokenCommandHandler : IRequestHandler<GeneratePasswordResetTokenCommand, string>
    {
        private readonly IUserService _userService;

        public GeneratePasswordResetTokenCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> Handle(GeneratePasswordResetTokenCommand request, CancellationToken cancellationToken)
        {
            return await _userService.GeneratePasswordResetTokenAsync(request.Email);
        }
    }
}
