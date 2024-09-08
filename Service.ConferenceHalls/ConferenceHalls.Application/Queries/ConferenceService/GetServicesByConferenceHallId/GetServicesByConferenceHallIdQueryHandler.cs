using MediatR;
using ConferenceHalls.Domain.Abstraction;
using CSharpFunctionalExtensions;
using AutoMapper;
using ConferenceHalls.Application.ViewModels;

namespace ConferenceHalls.Application.Queries
{
    public class GetServicesByConferenceHallIdQueryHandler 
        : IRequestHandler<GetServicesByConferenceHallIdQuery, Result<IEnumerable<ConferenceServiceViewModel>>>
    {
        private readonly IConferenceHallRepository _conferenceHallRepository;
        private readonly IConferenceServiceRepository _conferenceServiceRepository;
        private readonly IMapper _mapper;

        public GetServicesByConferenceHallIdQueryHandler(
            IConferenceHallRepository conferenceHallRepository,
            IConferenceServiceRepository conferenceServiceRepository,
            IMapper mapper)
        {
            _conferenceHallRepository = conferenceHallRepository;
            _conferenceServiceRepository = conferenceServiceRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ConferenceServiceViewModel>>> Handle(
            GetServicesByConferenceHallIdQuery request, 
            CancellationToken cancellationToken
        )
        {
            var hallResult = await _conferenceHallRepository.GetByIdsAsync(new[] { request.ConferenceHallId });
            if (hallResult.IsFailure)
            {
                return Result.Failure<IEnumerable<ConferenceServiceViewModel>>(hallResult.Error);
            }

            var hall = hallResult.Value!.FirstOrDefault();
            if(hall == null)
            {
                return Result.Failure<IEnumerable<ConferenceServiceViewModel>>("Conference hall wasn`t found");
            }
            
            var servicesResult = await _conferenceServiceRepository.GetByIdsAsync(hall.Services.Select(s => s.Id));
            if (servicesResult.IsFailure)
            {
                return Result.Failure<IEnumerable<ConferenceServiceViewModel>>(servicesResult.Error);
            }

            var services = servicesResult.Value;
            var serviceViewModels = _mapper.Map<IEnumerable<ConferenceServiceViewModel>>(services);

            return Result.Success(serviceViewModels);
        }
    }
}
