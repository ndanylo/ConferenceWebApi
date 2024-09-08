using CSharpFunctionalExtensions;
using MediatR;
using System;

namespace Bookings.Application.Commands
{
    public class DeleteBookingsByConferenceHallIdCommand : IRequest<Result>
    {
        public Guid ConferenceHallId { get; set; }
    }
}
