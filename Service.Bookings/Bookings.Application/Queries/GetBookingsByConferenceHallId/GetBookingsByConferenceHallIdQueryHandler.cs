using AutoMapper;
using Bookings.Domain.Abstractions;
using Bookings.Application.ViewModels;
using CSharpFunctionalExtensions;
using MediatR;
using Bookings.Application.Messaging.Services;

namespace Bookings.Application.Queries
{
    public class GetBookingsByConferenceHallIdQueryHandler 
        : IRequestHandler<GetBookingsByConferenceHallIdQuery, Result<IEnumerable<BookingViewModel>>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IConferenceHallService _сonferenceHallService;
        private readonly IMapper _mapper;

        public GetBookingsByConferenceHallIdQueryHandler(
            IBookingRepository bookingRepository, 
            IMapper mapper,
            IConferenceHallService сonferenceHallService
        )
        {
            _сonferenceHallService = сonferenceHallService;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<BookingViewModel>>> Handle(
            GetBookingsByConferenceHallIdQuery request, 
            CancellationToken cancellationToken
        )
        {
            var getBookingsResult = await _bookingRepository.GetByConferenceHallIdAsync(request.ConferenceHallId);
            if (getBookingsResult.IsFailure)
            {
                return Result.Failure<IEnumerable<BookingViewModel>>(getBookingsResult.Error);
            }

            var bookings = getBookingsResult.Value;
            if (bookings == null || !bookings.Any())
            {
                return Result.Failure<IEnumerable<BookingViewModel>>("No bookings were found for the specified conference hall");
            }

            var getConferenceResult = await _сonferenceHallService.GetConferenceHallsByIdsAsync(new [] {request.ConferenceHallId});
            if(getConferenceResult.IsFailure)
            {
                return Result.Failure<IEnumerable<BookingViewModel>>(getConferenceResult.Error);
            }

            var conferenceHall = getConferenceResult.Value.FirstOrDefault();
            if(conferenceHall == null)
            {
                return Result.Failure<IEnumerable<BookingViewModel>>("Conference hall wasn`t found");
            }
            
            var bookingsViewModel = new List<BookingViewModel>();

            foreach (var booking in bookings)
            {
                var selectedServicesResult = await _сonferenceHallService.GetConferenceServicesByIdsAsync(booking.SelectedServices);
                if (selectedServicesResult.IsFailure)
                {
                    return Result.Failure<IEnumerable<BookingViewModel>>("Error while receiving conference services!");
                }

                var bookingViewModel = _mapper.Map<BookingViewModel>(booking, opt =>
                {
                    opt.Items["ConferenceHall"] = conferenceHall;
                    opt.Items["SelectedServices"] = selectedServicesResult.Value;
                });

                bookingsViewModel.Add(bookingViewModel);
            }

            return Result.Success(bookingsViewModel.AsEnumerable());
        }
    }
}
