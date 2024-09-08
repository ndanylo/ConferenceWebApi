using ConferenceHalls.Domain.Abstraction;
using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceHalls.Application.ConferenceHalls.Commands
{
    public class AddServicesToConferenceHallCommandHandler 
        : IRequestHandler<AddServicesToConferenceHallCommand, Result>
    {
        private readonly IConferenceHallRepository _repository;
        private readonly IConferenceServiceRepository _serviceRepository;

        public AddServicesToConferenceHallCommandHandler(
            IConferenceHallRepository repository, 
            IConferenceServiceRepository serviceRepository
        )
        {
            _repository = repository;
            _serviceRepository = serviceRepository;
        }

        public async Task<Result> Handle(AddServicesToConferenceHallCommand request, CancellationToken cancellationToken)
        {
            var hallsResult = await _repository.GetByIdsAsync(new[] { request.Id });
            var hall = hallsResult.Value?.FirstOrDefault();

            if (hall == null)
            {
                return Result.Failure("Conference hall not found.");
            }

            var servicesResult = await _serviceRepository.GetByIdsAsync(request.ServiceIds);
            if (servicesResult.IsFailure)
            {
                return Result.Failure($"Error while retrieving services: {servicesResult.Error}");
            }

            var services = servicesResult.Value.ToList();
            var invalidServiceIds = request.ServiceIds.Except(services.Select(s => s.Id)).ToList();

            if (invalidServiceIds.Any())
            {
                return Result.Failure($"Services were not found: {string.Join(", ", invalidServiceIds)}");
            }

            var addServicesResult = hall.AddServices(services);
            if(addServicesResult.IsFailure)
            {
                return Result.Failure(addServicesResult.Error);
            }

            var updateResult = await _repository.UpdateAsync(hall);
            if (updateResult.IsFailure)
            {
                return Result.Failure("Error occurs while adding services to the conference hall");
            }
            return Result.Success();
        }
    }
}
