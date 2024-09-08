using MediatR;
using CSharpFunctionalExtensions;
using ConferenceHalls.Application.ViewModels;

namespace ConferenceHalls.Application.Queries
{
    public class GetServicesByIdsQuery : IRequest<Result<IEnumerable<ConferenceServiceViewModel>>>
    {
       public IEnumerable<Guid> ServiceIds { get; set; } = new List<Guid>();
    }
}
