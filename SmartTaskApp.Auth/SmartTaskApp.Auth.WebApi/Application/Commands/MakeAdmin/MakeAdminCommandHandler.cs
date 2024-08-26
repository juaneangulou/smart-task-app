using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.Auth.WebApi.Application.Commands.MakeAdmin
{
    public class MakeAdminCommandHandler : IRequestHandler<MakeAdminCommand, bool>
    {
        private readonly UserManager<User> _userManager;

        public MakeAdminCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(MakeAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return false; 
            }

            var result = await _userManager.AddToRoleAsync(user, "Admin");
            return result.Succeeded;
        }
    }
}
