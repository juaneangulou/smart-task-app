using Microsoft.AspNetCore.Identity;
using SmartTaskApp.Auth.WebApi.Domain.Repositories;

namespace SmartTaskApp.Auth.WebApi.Infraestructure.Data
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<string> GetByNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role?.Name;
        }

        public async Task AddAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
