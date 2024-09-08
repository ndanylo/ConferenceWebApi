using MediatR;
using CSharpFunctionalExtensions;
using ConferenceHalls.Application.ViewModels;

namespace ConferenceHalls.Application.Queries
{
    public class GetAllConferenceHallsQuery 
        : IRequest<Result<IEnumerable<ConferenceHallViewModel>>> {   }
}
