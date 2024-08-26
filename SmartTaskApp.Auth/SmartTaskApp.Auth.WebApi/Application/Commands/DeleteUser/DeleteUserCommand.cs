using MediatR;

namespace SmartTaskApp.Auth.WebApi.Application.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }
}
