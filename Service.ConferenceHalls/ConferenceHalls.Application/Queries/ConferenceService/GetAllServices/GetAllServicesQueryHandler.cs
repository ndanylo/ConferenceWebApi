using MediatR;
using ConferenceHalls.Domain.Abstraction;
using CSharpFunctionalExtensions;
using AutoMapper;
using ConferenceHalls.Application.ViewModels;

namespace ConferenceHalls.Application.Queries
{
    public class GetAllServicesQueryHandler
        : IRequestHandler<GetAllServicesQuery, Result<IEnumerable<ConferenceServiceViewModel>>>
    {
        private readonly IConferenceServiceRepository _repository;
        private readonly IMapper _mapper;

        public GetAllServicesQueryHandler(IConferenceServiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ConferenceServiceViewModel>>> Handle(
            GetAllServicesQuery request, 
            CancellationToken cancellationToken
        )
        {
            var result = await _repository.GetAllAsync();

            if (result.IsFailure)
            {
                return Result.Failure<IEnumerable<ConferenceServiceViewModel>>(result.Error);
            }

            var viewModels = _mapper.Map<IEnumerable<ConferenceServiceViewModel>>(result.Value);

            return Result.Success(viewModels);
        }
    }
}
