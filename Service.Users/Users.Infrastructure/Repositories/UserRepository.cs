using Users.Domain.Abstractions;
using Users.Domain.Entities;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Users.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result<IdentityResult>> RegisterAsync(User user, string password)
        {
            try
            {
                var userCreationResult = await _userManager.CreateAsync(user, password);
                return Result.Success(userCreationResult);
            }
            catch(Exception ex)
            {
                return Result.Failure<IdentityResult>(ex.Message);
            }
        }

        public async Task<Result<SignInResult>> LoginAsync(string username, string password)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    return SignInResult.Failed;
                }

                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
                return Result.Success(signInResult);
            }
            catch(Exception ex)
            {
                return Result.Failure<SignInResult>(ex.Message);
            }
        }

        public async Task<Result<User?>> FindByEmailAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                return Result.Success(user);
            }
            catch(Exception ex)
            {
                return Result.Failure<User?>(ex.Message);
            }
        }

        public async Task<Result<List<User>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                return Result.Success(users);
            }
            catch(Exception ex)
            {
                return Result.Failure<List<User>>(ex.Message);
            }
        }
    }
}