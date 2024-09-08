using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Users.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        private User() : base() { }

        private User(
            string email,
            Guid userId,
            string firstName, 
            string lastName
        )
        {
            Email = email;
            UserName = email;
            FirstName = firstName;
            LastName = lastName;
            Id = userId;
        }

        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;

        public static Result<User> Create(
            string email,
            string firstName, 
            string lastName, 
            Guid userId
        )
        {
            if (string.IsNullOrEmpty(email))
            {
                return Result.Failure<User>("email can not be null or empty");
            }

            if (string.IsNullOrEmpty(firstName))
            {
                return Result.Failure<User>("email can not be null or empty");
            }

            if (string.IsNullOrEmpty(lastName))
            {
                return Result.Failure<User>("userName can not be null or empty");
            }

            if (userId == Guid.Empty)
            {
                return Result.Failure<User>("id can not be null or empty");
            }
            return Result.Success(new User(email, userId, firstName, lastName));
        }
    }
}