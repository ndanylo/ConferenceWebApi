using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceHalls.Application.ConferenceHalls.Commands
{
    public class UpdateRentPriceCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public decimal RentPrice { get; set; }
    }
}
