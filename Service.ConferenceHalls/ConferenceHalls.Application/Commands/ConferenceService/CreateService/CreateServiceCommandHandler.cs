using MediatR;
using ConferenceHalls.Domain.Abstraction;
using CSharpFunctionalExtensions;

namespace ConferenceHalls.Application.Commands
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Result<Guid>>
    {
        private readonly IConferenceServiceRepository _repository;

        public CreateServiceCommandHandler(IConferenceServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var createServiceResult = await _repository.CreateAsync(request.Name, request.Price);
            if (createServiceResult.IsFailure)
            {
                return Result.Failure<Guid>(createServiceResult.Error);
            }

            return Result.Success(Guid.NewGuid());
        }
    }
}
