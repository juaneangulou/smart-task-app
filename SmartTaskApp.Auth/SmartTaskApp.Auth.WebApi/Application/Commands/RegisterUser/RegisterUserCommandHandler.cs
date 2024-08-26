using MediatR;
using SmartTaskApp.Auth.WebApi.Domain.Services;

namespace SmartTaskApp.Auth.WebApi.Application.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserService _userService;

        public RegisterUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.RegisterUserAsync(request.Email, request.Password, request.FirstName, request.LastName, request.DateOfBirth);
        }
    }
}