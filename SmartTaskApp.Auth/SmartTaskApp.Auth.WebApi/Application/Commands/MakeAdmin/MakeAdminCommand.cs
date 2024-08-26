using MediatR;

namespace SmartTaskApp.Auth.WebApi.Application.Commands.MakeAdmin
{
    public class MakeAdminCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }
}
