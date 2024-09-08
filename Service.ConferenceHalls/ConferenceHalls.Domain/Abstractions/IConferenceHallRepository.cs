using ConferenceHalls.Domain.Entities;
using ConferenceHalls.Domain.ValueObjects.ConferenceHall;
using CSharpFunctionalExtensions;

namespace ConferenceHalls.Domain.Abstraction
{
    public interface IConferenceHallRepository
    {
        Task<Result<IEnumerable<ConferenceHall>>> GetAllAsync();
        Task<Result<IEnumerable<ConferenceHall>>> GetByIdsAsync(IEnumerable<Guid> Ids);
        Task<Result> DeleteAsync(Guid conferenceHallId);
        Task<Result> CreateAsync(
            string name,
            int capacity,
            List<ConferenceService> services,
            decimal rentPrice
        );
        Task<Result> AddAsync(ConferenceHall conferenceHall);
        Task<Result<IEnumerable<ConferenceHall>>> GetHallsByCapacityAsync(Capacity capacitry);
        Task<Result> UpdateAsync(ConferenceHall conferenceHall);
    }
}