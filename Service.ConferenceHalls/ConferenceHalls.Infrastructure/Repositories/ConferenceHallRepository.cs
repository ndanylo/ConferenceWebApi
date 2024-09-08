using ConferenceHalls.Domain.Abstraction;
using ConferenceHalls.Domain.Entities;
using ConferenceHalls.Domain.ValueObjects.ConferenceHall;
using ConferenceHalls.Infrastructure.Persistence;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace ConferenceHalls.Infrastructure.Repositories
{
    public class ConferenceHallRepository : IConferenceHallRepository
    {
        private readonly ConferenceDbContext _dbContext;

        public ConferenceHallRepository(ConferenceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<ConferenceHall>>> GetAllAsync()
        {
            try
            {
                var halls = await _dbContext.ConferenceHalls
                    .Include(hall => hall.Services)
                    .ToListAsync();
                return Result.Success((IEnumerable<ConferenceHall>)halls);
            }
            catch(Exception ex)
            {
                return Result.Failure<IEnumerable<ConferenceHall>>(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<ConferenceHall>>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            try
            {
                var halls = await _dbContext.ConferenceHalls
                .Where(h => ids.Contains(h.Id))
                .Include(hall => hall.Services)
                .ToListAsync();
            
                return Result.Success((IEnumerable<ConferenceHall>)halls);
            }
            catch(Exception ex)
            {
                return Result.Failure<IEnumerable<ConferenceHall>>(ex.Message);
            }
        }

        public async Task<Result> DeleteAsync(Guid conferenceHallId)
        {
            try
            {
                var hall = await _dbContext.ConferenceHalls.FindAsync(conferenceHallId);
            
                if (hall == null)
                {
                    return Result.Failure("Conference hall not found.");
                }
                
                _dbContext.ConferenceHalls.Remove(hall);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> CreateAsync(
            string name, 
            int capacity, 
            List<ConferenceService> services, 
            decimal rentPrice
        )
        {
            try
            {
                var createHallResult = ConferenceHall.Create(
                    Guid.NewGuid(),
                    name, 
                    capacity, 
                    services, 
                    rentPrice
                );
                if(createHallResult.IsFailure)
                {
                    return Result.Failure($"Error while creating a hall: {createHallResult.Error}");
                }
                await _dbContext.ConferenceHalls.AddAsync(createHallResult.Value);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<ConferenceHall>>> GetHallsByCapacityAsync(Capacity capacity)
        {
            try
            {
                var halls = await _dbContext.ConferenceHalls
                    .Where(hall => hall.Capacity >= capacity)
                    .Include(hall => hall.Services)
                    .ToListAsync();
                
                return Result.Success(halls.AsEnumerable());
            }
            catch(Exception ex)
            {
                return Result.Failure<IEnumerable<ConferenceHall>>(ex.Message);
            }
        }

        public async Task<Result> AddAsync(ConferenceHall conferenceHall)
        {
            try
            {
                await _dbContext.ConferenceHalls.AddAsync(conferenceHall);

                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> UpdateAsync(ConferenceHall conferenceHall)
        {
            try
            {
                _dbContext.ConferenceHalls.Update(conferenceHall);
                
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
