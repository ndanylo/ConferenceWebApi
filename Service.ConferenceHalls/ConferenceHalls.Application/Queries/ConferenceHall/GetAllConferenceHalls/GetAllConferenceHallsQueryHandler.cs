using MediatR;
using AutoMapper;
using ConferenceHalls.Domain.Abstraction;
using CSharpFunctionalExtensions;
using ConferenceHalls.Application.ViewModels;

namespace ConferenceHalls.Application.Queries
{
    public class GetAllConferenceHallsQueryHandler 
        : IRequestHandler<GetAllConferenceHallsQuery, Result<IEnumerable<ConferenceHallViewModel>>>
    {
        private readonly IConferenceHallRepository _repository;
        private readonly IMapper _mapper;

        public GetAllConferenceHallsQueryHandler(IConferenceHallRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ConferenceHallViewModel>>> Handle(
            GetAllConferenceHallsQuery request, 
            CancellationToken cancellationToken
        )
        {
            var result = await _repository.GetAllAsync();
            if (result.IsFailure)
            {
                return Result.Failure<IEnumerable<ConferenceHallViewModel>>(result.Error);
            }

            var viewModels = _mapper.Map<List<ConferenceHallViewModel>>(result.Value);
            return Result.Success(viewModels.AsEnumerable());
        }
    }
}
