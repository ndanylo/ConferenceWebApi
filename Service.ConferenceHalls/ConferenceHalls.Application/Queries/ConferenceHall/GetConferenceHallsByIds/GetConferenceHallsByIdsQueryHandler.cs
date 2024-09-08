using MediatR;
using ConferenceHalls.Domain.Abstraction;
using CSharpFunctionalExtensions;
using AutoMapper;
using ConferenceHalls.Application.ViewModels;

namespace ConferenceHalls.Application.Queries
{
    public class GetConferenceHallsByIdsQueryHandler
        : IRequestHandler<GetConferenceHallsByIdsQuery, Result<IEnumerable<ConferenceHallViewModel>>>
    {
        private readonly IConferenceHallRepository _repository;
        private readonly IMapper _mapper;

        public GetConferenceHallsByIdsQueryHandler(IConferenceHallRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ConferenceHallViewModel>>> Handle(
            GetConferenceHallsByIdsQuery request, 
            CancellationToken cancellationToken
        )
        {
            var result = await _repository.GetByIdsAsync(request.Ids);

            if (result.IsFailure)
            {
                return Result.Failure<IEnumerable<ConferenceHallViewModel>>(result.Error);
            }

            var viewModels = _mapper.Map<IEnumerable<ConferenceHallViewModel>>(result.Value);

            return Result.Success(viewModels);
        }
    }
}
