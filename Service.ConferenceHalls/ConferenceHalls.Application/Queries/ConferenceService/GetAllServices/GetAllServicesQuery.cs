using MediatR;
using CSharpFunctionalExtensions;
using ConferenceHalls.Application.ViewModels;

namespace ConferenceHalls.Application.Queries
{
    public class GetAllServicesQuery 
        : IRequest<Result<IEnumerable<ConferenceServiceViewModel>>> {   }
}
