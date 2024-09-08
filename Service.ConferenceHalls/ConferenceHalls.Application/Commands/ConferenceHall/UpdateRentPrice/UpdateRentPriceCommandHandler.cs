using ConferenceHalls.Domain.Abstraction;
using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceHalls.Application.ConferenceHalls.Commands
{
    public class UpdateRentPriceCommandHandler : IRequestHandler<UpdateRentPriceCommand, Result>
    {
        private readonly IConferenceHallRepository _repository;

        public UpdateRentPriceCommandHandler(IConferenceHallRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateRentPriceCommand request, CancellationToken cancellationToken)
        {
            var hallsResult = await _repository.GetByIdsAsync(new[] { request.Id });
            var hall = hallsResult.Value?.FirstOrDefault();

            if (hall == null)
            {
                return Result.Failure("Conference hall wasn`t found.");
            }

            hall.UpdateRentPrice(request.RentPrice);
            var updateResult = await _repository.UpdateAsync(hall);
            if (updateResult.IsFailure)
            {
                return Result.Failure($"Error occurs while updating the rent price of the conference hall with id {request.Id}");
            }
            return Result.Success();
        }
    }
}
