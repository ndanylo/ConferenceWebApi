using ConferenceHalls.Domain.Entities;
using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceHalls.Application.Commands
{
    public class RestoreConferenceHallCommand : IRequest<Result>
    {
        public ConferenceHall ConferenceHall { get; }

        public RestoreConferenceHallCommand(ConferenceHall conferenceHall)
        {
            ConferenceHall = conferenceHall;
        }
    }
}