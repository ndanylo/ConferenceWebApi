using MediatR;
using CSharpFunctionalExtensions;

namespace ConferenceHalls.Application.Commands
{
    public class CreateServiceCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
