using AutoMapper;
using Bookings.Domain.Abstractions;
using MediatR;
using Bookings.Application.ViewModels;
using CSharpFunctionalExtensions;
using Bookings.Application.Messaging.Services;

namespace Bookings.Application.Queries
{
    public class GetBookingsByUserIdQueryHandler : IRequestHandler<GetBookingsByUserIdQuery, Result<IEnumerable<BookingViewModel>>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IConferenceHallService _сonferenceHallService;
        private readonly IMapper _mapper;

        public GetBookingsByUserIdQueryHandler(
            IBookingRepository bookingRepository,
            IConferenceHallService сonferenceHallService,
            IMapper mapper)
        {
            _сonferenceHallService = сonferenceHallService;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<BookingViewModel>>> Handle(GetBookingsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var getBookingsResult = await _bookingRepository.GetByUserIdAsync(request.UserId);
            if(getBookingsResult.IsFailure)
            {
                return Result.Failure<IEnumerable<BookingViewModel>>(getBookingsResult.Error);
            }
            var bookings = getBookingsResult.Value;

            if (bookings == null || !bookings.Any())
            {
                return Result.Failure<IEnumerable<BookingViewModel>>("No bookings were found for the specified user");
            }

            var bookingsViewModel = new List<BookingViewModel>();

            foreach (var booking in bookings)
            {
                var conferenceHallResult = await _сonferenceHallService.GetConferenceHallsByIdsAsync(new[] { booking.ConferenceHallId });
                if (conferenceHallResult.IsFailure)
                {
                    return Result.Failure<IEnumerable<BookingViewModel>>("Error while receiving conference halls!");
                }

                var conferenceHall = conferenceHallResult.Value.FirstOrDefault();
                if (conferenceHall == null)
                {
                    return Result.Failure<IEnumerable<BookingViewModel>>("Conference hall wasn`t found");
                }

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
