using ConferenceHalls.Application.ViewModels;
using ConferenseHalls.Application.Messaging.Services;
using CSharpFunctionalExtensions;
using MassTransit;
using RabbitMQ.Contracts.Requests;
using RabbitMQ.Contracts.Responses;

namespace ConferenceHalls.Application.Messaging.Services.Booking
{
    public class BookingService : IBookingService
    {
        private readonly IRequestClient<DeleteBookingsByConferenceHallIdRequest> _bookingsDeleteClient;
        private readonly IRequestClient<RemoveServiceFromBookingsRequest> _removeServiceFromBookingsClient;

        public BookingService(
            IRequestClient<DeleteBookingsByConferenceHallIdRequest> bookingsDeleteClient,
            IRequestClient<RemoveServiceFromBookingsRequest> removeServiceFromBookingsClient
        )
        {
            _removeServiceFromBookingsClient = removeServiceFromBookingsClient;
            _bookingsDeleteClient = bookingsDeleteClient;
        }
        
        public async Task<Result<bool>> DeleteBookingByConferenceHallId(Guid conferenceHallId)
        {
            try
            {
                var response = await _bookingsDeleteClient
                    .GetResponse<DeleteBookingsByConferenceHallResponse>(new DeleteBookingsByConferenceHallIdRequest
                    {
                        ConferenceHallId = conferenceHallId
                    });
                if(response.Message.IsSuccess)
                {
                    return Result.Success(true);
                }
                return Result.Success(false);
            }
            catch(Exception ex)
            {
                return Result.Failure<bool>(ex.Message);
            }
        }
        
        public async Task<Result> RemoveServiceFromBookings(ConferenceServiceViewModel service)
        {
            try
            {
                var response = await _removeServiceFromBookingsClient
                    .GetResponse<RemoveServiceFromBookingsResponse>(new RemoveServiceFromBookingsRequest
                    {
                        Service = service
                    });
                
                if(response.Message.IsSuccess)
                {
                    return Result.Success();
                }
                else
                {
                    return Result.Failure(response.Message.Error);
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}