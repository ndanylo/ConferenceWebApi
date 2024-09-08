using MediatR;
using CSharpFunctionalExtensions;
using AutoMapper;
using ConferenceHalls.Domain.Abstraction;
using ConferenceHalls.Application.ViewModels;

namespace ConferenceHalls.Application.Queries
{
    public class GetConferenceHallsByCapacityQueryHandler 
        : IRequestHandler<GetConferenceHallsByCapacityQuery, Result<IEnumerable<ConferenceHallViewModel>>>
    {
        private readonly IConferenceHallRepository _hallRepository;
        private readonly IMapper _mapper;

        public GetConferenceHallsByCapacityQueryHandler(
            IConferenceHallRepository hallRepository,
            IMapper mapper)
        {
            _hallRepository = hallRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ConferenceHallViewModel>>> Handle(
            GetConferenceHallsByCapacityQuery request, 
            CancellationToken cancellationToken
        )
        {
            var getHallsResult = await _hallRepository.GetHallsByCapacityAsync(request.Capacity);
            if (getHallsResult.IsFailure)
            {
                return Result.Failure<IEnumerable<ConferenceHallViewModel>>(getHallsResult.Error);
            }

            var halls = getHallsResult.Value;
            var hallsViewModel = _mapper.Map<IEnumerable<ConferenceHallViewModel>>(halls);

            return Result.Success(hallsViewModel);
        }
    }
}
