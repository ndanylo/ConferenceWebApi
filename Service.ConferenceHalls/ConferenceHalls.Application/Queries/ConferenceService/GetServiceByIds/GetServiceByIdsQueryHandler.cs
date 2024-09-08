using MediatR;
using ConferenceHalls.Domain.Abstraction;
using CSharpFunctionalExtensions;
using AutoMapper;
using ConferenceHalls.Application.ViewModels;

namespace ConferenceHalls.Application.Queries
{
    public class GetServicesByIdsQueryHandler 
        : IRequestHandler<GetServicesByIdsQuery, Result<IEnumerable<ConferenceServiceViewModel>>>
    {
        private readonly IConferenceServiceRepository _conferenceServiceRepository;
        private readonly IMapper _mapper;

        public GetServicesByIdsQueryHandler(
            IConferenceServiceRepository conferenceServiceRepository,
            IMapper mapper)
        {
            _conferenceServiceRepository = conferenceServiceRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ConferenceServiceViewModel>>> Handle(
            GetServicesByIdsQuery request, 
            CancellationToken cancellationToken
        )
        {
            var serviceResult = await _conferenceServiceRepository.GetByIdsAsync(request.ServiceIds);
            if (serviceResult.IsFailure)
            {
                return Result.Failure<IEnumerable<ConferenceServiceViewModel>>(serviceResult.Error);
            }

            var service = serviceResult.Value;
            var serviceViewModel = _mapper.Map<List<ConferenceServiceViewModel>>(service.ToList());

            return Result.Success(serviceViewModel.AsEnumerable());
        }
    }
}
