using ConferenceHalls.Domain.Abstraction;
using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceHalls.Application.ConferenceHalls.Commands
{
    public class CreateConferenceHallCommandHandler : IRequestHandler<CreateConferenceHallCommand, Result>
    {
        private readonly IConferenceHallRepository _repository;
        private readonly IConferenceServiceRepository _conferenceServiceRepository;

        public CreateConferenceHallCommandHandler(
            IConferenceHallRepository repository,
            IConferenceServiceRepository conferenceServiceRepository
        )
        {
            _conferenceServiceRepository = conferenceServiceRepository;
            _repository = repository;
        }

        public async Task<Result> Handle(
            CreateConferenceHallCommand request, 
            CancellationToken cancellationToken
        )
        {
            var serviceResults = await _conferenceServiceRepository.GetByIdsAsync(request.ServiceIds);

            if (serviceResults.IsFailure)
            {
                return Result.Failure($"Error while retrieving conference services: {serviceResults.Error}");
            }

            var services = serviceResults.Value.ToList();
            var invalidServiceIds = request.ServiceIds.Except(services.Select(s => s.Id)).ToList();

            if (invalidServiceIds.Any())
            {
                return Result.Failure($"Service IDs were not found: {string.Join(", ", invalidServiceIds)}");
            }

            var createConferenceHallResult = await _repository.CreateAsync(
                request.Name,
                request.Capacity,
                services,
                request.RentPrice);

            if(createConferenceHallResult.IsFailure)
            {
                return Result.Failure($"Error occurred while creating the conference hall with name {request.Name}");
            }

            return Result.Success();
        }
    }
}
