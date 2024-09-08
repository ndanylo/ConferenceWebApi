using MediatR;
using CSharpFunctionalExtensions;

namespace ConferenceHalls.Application.Commands
{
    public class DeleteServiceCommand : IRequest<Result>
    {
        public Guid ServiceId { get; set; }
    }
}
