using Bookings.Application.Services.Abstractions;
using Bookings.Domain.Abstractions;
using Bookings.Domain.Entities;
using Bookings.Application.Messaging.Services;
using CSharpFunctionalExtensions;
using MediatR;

namespace Bookings.Application.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result<Guid>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IConferenceHallService _conferenceHallService;
        private readonly IPriceAdjustmentService _priceAdjustmentService;

        public CreateBookingCommandHandler(
            IBookingRepository bookingRepository,
            IConferenceHallService conferenceHallService,
            IPriceAdjustmentService priceAdjustmentService
        )
        {
            _priceAdjustmentService = priceAdjustmentService;
            _conferenceHallService = conferenceHallService; 
            _bookingRepository = bookingRepository;
        }

        public async Task<Result<Guid>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var conferenceHallResult = await _conferenceHallService.GetConferenceHallsByIdsAsync(new List<Guid> { request.ConferenceHallId });
            if (conferenceHallResult.IsFailure)
            {
                return Result.Failure<Guid>("Failed to retrieve conference hall information.");
            }

            var conferenceHall = conferenceHallResult.Value.FirstOrDefault();
            if (conferenceHall == null)
            {
                return Result.Failure<Guid>("Conference hall wasn`t found.");
            }

            var isAvailableResult = await _bookingRepository.IsHallAvailableAsync(
                request.ConferenceHallId,
                request.Date,
                request.StartTime,
                request.Duration
            );

            if (isAvailableResult.IsFailure || !isAvailableResult.Value)
            {
                return Result.Failure<Guid>("Conference hall wasn`t found");
            }

            var hallServiceIds = conferenceHall.Services.Select(service => service.Id).ToList();
            var invalidServiceIds = request.SelectedServices.Except(hallServiceIds).ToList();

            if (invalidServiceIds.Any())
            {
                return Result.Failure<Guid>("Some selected services are not available for the selected conference hall.");
            }
            var servicePrices = conferenceHall.Services
                .Where(service => request.SelectedServices.Contains(service.Id))
                .ToDictionary(service => service.Id, service => service.Price);

            decimal adjustedPricePerHour = _priceAdjustmentService.AdjustPrice(conferenceHall.RentPrice, request.Date, request.Duration);

            var createBookingResult = Booking.Create(
                Guid.NewGuid(),
                request.UserId,
                request.ConferenceHallId,
                request.Date,
                request.StartTime,
                request.Duration,
                request.SelectedServices,
                servicePrices,
                adjustedPricePerHour
            );

            if (createBookingResult.IsFailure)
            {
                return Result.Failure<Guid>(createBookingResult.Error);
            }

            var booking = createBookingResult.Value;
            await _bookingRepository.AddAsync(booking);

            return Result.Success(booking.Id);
        }
    }
}
