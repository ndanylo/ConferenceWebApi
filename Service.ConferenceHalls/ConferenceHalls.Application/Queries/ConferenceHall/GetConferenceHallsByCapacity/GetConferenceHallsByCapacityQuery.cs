using MediatR;
using CSharpFunctionalExtensions;
using ConferenceHalls.Application.ViewModels;
using ConferenceHalls.Domain.ValueObjects.ConferenceHall;

namespace ConferenceHalls.Application.Queries
{
    public class GetConferenceHallsByCapacityQuery : IRequest<Result<IEnumerable<ConferenceHallViewModel>>>
    {
        public required Capacity Capacity { get; set; }
    }
}
