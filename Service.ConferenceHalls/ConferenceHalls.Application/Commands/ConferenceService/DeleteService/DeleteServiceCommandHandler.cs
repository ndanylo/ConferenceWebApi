using MediatR;
using ConferenceHalls.Domain.Abstraction;
using CSharpFunctionalExtensions;
using AutoMapper;
using ConferenceHalls.Application.ViewModels;
using ConferenseHalls.Application.Messaging.Services;

namespace ConferenceHalls.Application.Commands
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, Result>
    {
        private readonly IConferenceServiceRepository _repository;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        public DeleteServiceCommandHandler(
            IConferenceServiceRepository repository,
            IBookingService bookingService,
            IMapper mapper
        )
        {
            _mapper = mapper;
            _bookingService = bookingService;
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var getServiceResult = await _repository.GetByIdsAsync(new[] { request.ServiceId });
            if (getServiceResult.IsFailure)
            {
                return Result.Failure($"Service with id {request.ServiceId} wasn`t found");
            }
            
            var service = getServiceResult.Value.FirstOrDefault();
            if (service == null)
            {
                return Result.Failure("Service wasn't found");
            }

            var deleteServiceResult = await _repository.DeleteAsync(request.ServiceId);
            if (deleteServiceResult.IsFailure)
            {
                return Result.Failure(deleteServiceResult.Error);
            }

            var removeServiceResult = await _bookingService
                .RemoveServiceFromBookings(_mapper.Map<ConferenceServiceViewModel>(service));
            if (removeServiceResult.IsFailure)
            {
                await _repository.AddAsync(service);
                return Result.Failure(removeServiceResult.Error);
            }

            return Result.Success();
        }
    }
}