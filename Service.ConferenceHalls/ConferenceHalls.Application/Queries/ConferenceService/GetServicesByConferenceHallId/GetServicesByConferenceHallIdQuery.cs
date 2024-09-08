using MediatR;
using CSharpFunctionalExtensions;
using ConferenceHalls.Application.ViewModels;

namespace ConferenceHalls.Application.Queries
{
    public class GetServicesByConferenceHallIdQuery 
        : IRequest<Result<IEnumerable<ConferenceServiceViewModel>>>
    {
        public Guid ConferenceHallId { get; }

        public GetServicesByConferenceHallIdQuery(Guid conferenceHallId)
        {
            ConferenceHallId = conferenceHallId;
        }
    }
}
