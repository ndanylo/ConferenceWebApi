using MediatR;
using CSharpFunctionalExtensions;

namespace ConferenceHalls.Application.ConferenceHalls.Commands
{
    public class DeleteConferenceHallCommand : IRequest<Result>
    {
        public Guid ConferenceHallId { get; set; }

        public DeleteConferenceHallCommand(Guid conferenceHallId)
        {
            ConferenceHallId = conferenceHallId;
        }
    }
}
