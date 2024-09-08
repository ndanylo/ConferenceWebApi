using ConferenceHalls.Domain.Abstraction;
using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceHalls.Application.Commands
{
    public class RestoreConferenceHallCommandHandler : IRequestHandler<RestoreConferenceHallCommand, Result>
    {
        private readonly IConferenceHallRepository _repository;

        public RestoreConferenceHallCommandHandler(IConferenceHallRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(RestoreConferenceHallCommand request, CancellationToken cancellationToken)
        {
            var addConferenceHallResult = await _repository.AddAsync(request.ConferenceHall);
            if(addConferenceHallResult.IsFailure)
            {
                return Result.Failure(addConferenceHallResult.Error);
            }

            return Result.Success();
        }
    }
}