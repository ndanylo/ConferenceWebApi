using Users.Domain.Entities;

namespace Users.Application.Services.Abstractions
{
    public interface IJwtService
    {
        Task<string> GenerateToken(User user);
    }
}