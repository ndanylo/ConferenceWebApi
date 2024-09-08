using ConferenceHalls.Domain.Abstraction;
using ConferenceHalls.Domain.Entities;
using ConferenceHalls.Infrastructure.Persistence;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace ConferenceHalls.Infrastructure.Repositories
{
    public class ConferenceServiceRepository : IConferenceServiceRepository
    {
        private readonly ConferenceDbContext _dbContext;

        public ConferenceServiceRepository(ConferenceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> CreateAsync(string name, decimal price)
        {
            try
            {
                var createServiceResult = ConferenceService.Create(name, price);
                if(createServiceResult.IsFailure)
                {
                    return Result.Failure($"Error while creating service: {createServiceResult.Error}");
                }

                await _dbContext.ConferenceServices.AddAsync(createServiceResult.Value);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> DeleteAsync(Guid serviceId)
        {
            try
            {
                var service = await _dbContext.ConferenceServices.FindAsync(serviceId);
                
                if (service == null)
                {
                    return Result.Failure("Conference service not found.");
                }
                
                _dbContext.ConferenceServices.Remove(service);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<ConferenceService>>> GetByIdsAsync(IEnumerable<Guid> serviceIds)
        {
            try
            {
                var services = await _dbContext.ConferenceServices
                    .Where(s => serviceIds.Contains(s.Id))
                    .ToListAsync();
                
                return Result.Success((IEnumerable<ConferenceService>)services);
            }
            catch(Exception ex)
            {
                return Result.Failure<IEnumerable<ConferenceService>>(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<ConferenceService>>> GetAllAsync()
        {
            try
            {
                var services = await _dbContext.ConferenceServices.ToListAsync();
                return Result.Success(services.AsEnumerable());
            }
            catch(Exception ex)
            {
                return Result.Failure<IEnumerable<ConferenceService>>(ex.Message);
            }
        }

        public async Task<Result> AddAsync(ConferenceService service)
        {
            try
            {
                await _dbContext.ConferenceServices.AddAsync(service);
                
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
