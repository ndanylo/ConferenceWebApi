using AutoMapper;
using ConferenceHalls.Domain.Entities;
using ConferenceHalls.Application.ViewModels;

namespace ConferenceHalls.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ConferenceHall, ConferenceHallViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
                .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity.Value))
                .ForMember(dest => dest.RentPrice, opt => opt.MapFrom(src => src.RentPrice.Value))
                .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services));

            CreateMap<ConferenceService, ConferenceServiceViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Value));
        }
    }
}
