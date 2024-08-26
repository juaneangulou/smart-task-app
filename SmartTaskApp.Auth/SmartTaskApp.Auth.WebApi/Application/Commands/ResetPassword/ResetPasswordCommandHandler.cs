using MediatR;
using SmartTaskApp.Auth.WebApi.Domain.Services;

namespace SmartTaskApp.Auth.WebApi.Application.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IUserService _userService;

        public ResetPasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);
        }
    }
}
