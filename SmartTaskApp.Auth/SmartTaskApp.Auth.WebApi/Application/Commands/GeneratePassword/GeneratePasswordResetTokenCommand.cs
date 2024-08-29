using MediatR;

namespace SmartTaskApp.Auth.WebApi.Application.Commands.GeneratePassword
{
    public class GeneratePasswordResetTokenCommand : IRequest<string>
    {
        public string Email { get; set; }
    }

}
