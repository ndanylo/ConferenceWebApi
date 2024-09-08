using MediatR;
using Users.Domain.Abstractions;
using Users.Domain.Entities;
using CSharpFunctionalExtensions;
using Users.Infrastructure.Persistence.Services.Abstractions;

namespace Users.Application.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IRoleService roleService
        )
        {
            _roleService = roleService;
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            Guid userGuid = Guid.NewGuid();

            var userResult = User.Create(
                request.Email,
                request.FirstName,
                request.LastName,
                userGuid
            );
            if (userResult.IsFailure)
            {
                return Result.Failure<Guid>(userResult.Error);
            }

            var user = userResult.Value;
            var registerResult = await _userRepository.RegisterAsync(user, request.Password);
            if(registerResult.IsFailure)
            {
                return Result.Failure<Guid>(registerResult.Error);
            }
            
            var register = registerResult.Value;
            if (!register.Succeeded)
            {
                var errors = string.Join(", ", register.Errors.Select(e => e.Description));
                return Result.Failure<Guid>(errors);
            }

            var roleCheckResult = await _roleService.CreateRoleAsync("User");
            if (roleCheckResult.IsFailure)
            {
                return Result.Failure<Guid>(roleCheckResult.Error);
            }

            var assignRoleResult = await _roleService.AssignRoleToUserAsync(user, "User");
            if (assignRoleResult.IsFailure)
            {
                return Result.Failure<Guid>(assignRoleResult.Error);
            }

            return Result.Success(userGuid);
        }
    }
}