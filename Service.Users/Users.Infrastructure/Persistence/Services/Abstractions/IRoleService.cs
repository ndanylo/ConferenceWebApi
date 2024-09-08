using Users.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Users.Infrastructure.Persistence.Services.Abstractions
{
    public interface IRoleService
    {
        Task<Result> CreateRoleAsync(string roleName);
        Task<Result> AssignRoleToUserAsync(User user, string roleName);
        Task<bool> RoleExistsAsync(string roleName);
    }
}
