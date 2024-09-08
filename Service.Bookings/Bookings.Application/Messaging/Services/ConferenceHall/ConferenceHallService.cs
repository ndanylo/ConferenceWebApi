using Bookings.Application.ViewModels;
using CSharpFunctionalExtensions;
using MassTransit;
using RabbitMQ.Contracts.Requests;
using RabbitMQ.Contracts.Responses;

namespace Bookings.Application.Messaging.Services.ConferenceHall
{
    public class ConferenceHallService : IConferenceHallService
    {
        private readonly IRequestClient<GetConferenceHallsByIdsRequest> _getConferenceHallsByIds;
        private readonly IRequestClient<GetConferenceServicesByIdsRequest> _getConferenceServicesByIds;
        private readonly IRequestClient<GetConferenceHallsByCapacityRequest> _getConferenceHallsByCapacity;

        public ConferenceHallService(
            IRequestClient<GetConferenceHallsByIdsRequest> getConferenceHallsByIds,
            IRequestClient<GetConferenceServicesByIdsRequest> getConferenceServicesByIds,
            IRequestClient<GetConferenceHallsByCapacityRequest> getConferenceHallsByCapacity
        )
        {
            _getConferenceHallsByIds = getConferenceHallsByIds;
            _getConferenceServicesByIds = getConferenceServicesByIds;
            _getConferenceHallsByCapacity = getConferenceHallsByCapacity;
        }

        public async Task<Result<List<ConferenceHallViewModel>>> GetConferenceHallsByIdsAsync(IEnumerable<Guid> ids)
        {
            try
            {
                var response = await _getConferenceHallsByIds
                    .GetResponse<GetConferenceHallsByIdsResponse>(new GetConferenceHallsByIdsRequest
                    {
                        Ids = ids.ToArray()
                    });

                return Result.Success(response.Message.ConferenceHalls.ToList());
            }
            catch(Exception ex)
            {
                return Result.Failure<List<ConferenceHallViewModel>>(ex.Message);
            }
        }

        public async Task<Result<List<ConferenceServiceViewModel>>> GetConferenceServicesByIdsAsync(IEnumerable<Guid> ids)
        {
            try
            {
                var response = await _getConferenceServicesByIds
                    .GetResponse<GetConferenceServicesByIdsResponse>(new GetConferenceServicesByIdsRequest
                    {
                        Ids = ids.ToArray()
                    });

                return Result.Success(response.Message.ConferenceServices.ToList());
            }
            catch(Exception ex)
            {
                return Result.Failure<List<ConferenceServiceViewModel>>(ex.Message);
            }
        }

        public async Task<Result<List<ConferenceHallViewModel>>> GetConferenceHallsByCapacityAsync(int capacity)
        {
            try
            {
                var response = await _getConferenceHallsByCapacity
                    .GetResponse<GetConferenceHallsByCapacityResponse>(new GetConferenceHallsByCapacityRequest
                    {
                        Capacity = capacity
                    });

                return Result.Success(response.Message.ConferenceHalls.ToList());
            }
            catch (Exception ex)
            {
                return Result.Failure<List<ConferenceHallViewModel>>(ex.Message);
            }
        }
    }
}