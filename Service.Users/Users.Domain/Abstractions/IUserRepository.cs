using CSharpFunctionalExtensions;
using Users.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Users.Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<Result<IdentityResult>> RegisterAsync(User user, string password);
        Task<Result<SignInResult>> LoginAsync(string username, string password);
        Task<Result<User?>> FindByEmailAsync(string email);
        Task<Result<List<User>>> GetAllUsersAsync();
    }
}
