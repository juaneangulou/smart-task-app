using MediatR;

namespace SmartTaskApp.Auth.WebApi.Application.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
