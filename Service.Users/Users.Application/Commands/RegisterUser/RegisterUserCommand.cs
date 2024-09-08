using CSharpFunctionalExtensions;
using MediatR;

namespace Users.Application.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<Result<Guid>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}