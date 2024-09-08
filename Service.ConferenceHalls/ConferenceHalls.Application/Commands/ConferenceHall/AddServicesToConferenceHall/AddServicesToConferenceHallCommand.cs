using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceHalls.Application.ConferenceHalls.Commands
{
    public class AddServicesToConferenceHallCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public List<Guid> ServiceIds { get; set; } = new List<Guid>();
    }
}
