using AutoMapper;
using Bookings.Domain.Entities;
using Bookings.Application.ViewModels;

namespace Bookings.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Booking, BookingViewModel>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => (decimal)src.TotalPrice))
                .ForMember(dest => dest.ConferenceHall, opt => opt.MapFrom((src, dest, _, context) =>
                    context.Items.ContainsKey("ConferenceHall") 
                    ? context.Items["ConferenceHall"] as ConferenceHallViewModel 
                    : null))
                .ForMember(dest => dest.SelectedServices, opt => opt.MapFrom((src, dest, _, context) =>
                    context.Items.ContainsKey("SelectedServices") 
                    ? context.Items["SelectedServices"] as List<ConferenceServiceViewModel> 
                    : new List<ConferenceServiceViewModel>()));
        }
    }
}
