using MediatR;
using CSharpFunctionalExtensions;
using Bookings.Application.ViewModels;

namespace Bookings.Application.Commands
{
    public class RemoveServiceFromBookingsCommand : IRequest<Result>
    {
        public required ConferenceServiceViewModel Service { get; set; }
    }
}
