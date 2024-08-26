using MediatR;

namespace SmartTaskApp.Auth.WebApi.Application.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<(string accessToken, string refreshToken)>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
