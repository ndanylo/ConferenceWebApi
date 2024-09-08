using Bookings.Application.Queries;
using Bookings.Domain.Abstractions;
using Bookings.Application.Messaging.Services;
using CSharpFunctionalExtensions;
using MediatR;
using Bookings.Application.ViewModels;
using AutoMapper;

namespace Bookings.Application.Handlers
{
    public class SearchAvailableHallsQueryHandler 
        : IRequestHandler<SearchAvailableHallsQuery, Result<List<ConferenceHallViewModel>>>
    {
        private readonly IConferenceHallService _conferenceHallService;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public SearchAvailableHallsQueryHandler(
            IConferenceHallService conferenceHallService,
            IBookingRepository bookingRepository,
            IMapper mapper
        )
        {
            _mapper = mapper;
            _conferenceHallService = conferenceHallService;
            _bookingRepository = bookingRepository;
        }

        public async Task<Result<List<ConferenceHallViewModel>>> Handle(
            SearchAvailableHallsQuery request,
            CancellationToken cancellationToken
        )
        {
            var hallsResult = await _conferenceHallService.GetConferenceHallsByCapacityAsync(request.Capacity);
            if (hallsResult.IsFailure)
            {
                return Result.Failure<List<ConferenceHallViewModel>>(hallsResult.Error);
            }

            var availableHalls = new List<ConferenceHallViewModel>();

            foreach (var hall in hallsResult.Value)
            {
                var isAvailableResult = await _bookingRepository.IsHallAvailableAsync(
                    hall.Id, 
                    request.Date, 
                    request.StartTime, 
                    request.Duration
                );
                
                if (isAvailableResult.IsSuccess && isAvailableResult.Value)
                {
                    var hallDetailsResult = await _conferenceHallService.GetConferenceHallsByIdsAsync(new[] { hall.Id });
                    if (hallDetailsResult.IsFailure)
                    {
                        return Result.Failure<List<ConferenceHallViewModel>>("Error while receiving conference halls!");
                    }

                    var hallDetails = hallDetailsResult.Value.FirstOrDefault();
                    if (hallDetails == null)
                    {
                        return Result.Failure<List<ConferenceHallViewModel>>("Conference hall wasn`t found");
                    }

                    var servicesResult = await _conferenceHallService.GetConferenceServicesByIdsAsync(hall.Services.Select(hall => hall.Id));
                    if (servicesResult.IsFailure)
                    {
                        return Result.Failure<List<ConferenceHallViewModel>>("Error while receiving conference services!");
                    }

                    var hallViewModel = _mapper.Map<ConferenceHallViewModel>(hallDetails, opt =>
                    {
                        opt.Items["Services"] = servicesResult.Value;
                    });

                    availableHalls.Add(hallViewModel);
                }
            }

            return Result.Success(availableHalls);
        }
    }
}
