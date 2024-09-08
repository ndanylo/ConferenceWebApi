using ConferenceHalls.Domain.Entities;
using CSharpFunctionalExtensions;

namespace ConferenceHalls.Domain.Abstraction
{
    public interface IConferenceServiceRepository
    {
        Task<Result> CreateAsync(
            string name, 
            decimal price
        );
        Task<Result> DeleteAsync(Guid serviceId);
        Task<Result<IEnumerable<ConferenceService>>> GetByIdsAsync(IEnumerable<Guid> conferenceServiceIds);
        Task<Result> AddAsync(ConferenceService service);
        Task<Result<IEnumerable<ConferenceService>>> GetAllAsync();
    }
}