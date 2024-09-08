using Users.Domain.Entities;
using Users.Infrastructure.Persistence.Services.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Users.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleService(RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<Result> CreateRoleAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return Result.Success();
            }

            var role = new IdentityRole<Guid>(roleName);
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded 
                ? Result.Success() 
                : Result.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<Result> AssignRoleToUserAsync(User user, string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return Result.Failure($"Role '{roleName}' does not exist.");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded 
                ? Result.Success() 
                : Result.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
