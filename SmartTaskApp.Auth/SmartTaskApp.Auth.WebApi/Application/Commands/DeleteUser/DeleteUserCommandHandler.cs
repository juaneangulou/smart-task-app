using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.Auth.WebApi.Application.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly UserManager<User> _userManager;

        public DeleteUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return false; 
            }

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }

}
