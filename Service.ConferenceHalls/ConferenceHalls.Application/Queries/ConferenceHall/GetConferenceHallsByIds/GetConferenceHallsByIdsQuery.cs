using MediatR;
using CSharpFunctionalExtensions;
using ConferenceHalls.Application.ViewModels;

namespace ConferenceHalls.Application.Queries
{
    public class GetConferenceHallsByIdsQuery : IRequest<Result<IEnumerable<ConferenceHallViewModel>>>
    {
        public IEnumerable<Guid> Ids { get; set; } = new List<Guid>();
    }
}
