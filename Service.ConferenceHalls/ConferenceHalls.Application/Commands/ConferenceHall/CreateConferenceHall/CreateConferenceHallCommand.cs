using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceHalls.Application.ConferenceHalls.Commands
{
    public class CreateConferenceHallCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<Guid> ServiceIds { get; set; }
        public decimal RentPrice { get; set; }

        public CreateConferenceHallCommand(
            string name,
            int capacity,
            List<Guid> serviceIds,
            decimal rentPrice)
        {
            Name = name;
            Capacity = capacity;
            ServiceIds = serviceIds;
            RentPrice = rentPrice;
        }
    }
}
