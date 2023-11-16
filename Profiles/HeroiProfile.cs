using AutoMapper;
using HeroisApi.Dtos.HeroiDtos;
using HeroisApi.Models;

namespace HeroisApi.Profiles;

public class HeroiProfile : Profile
{
    public HeroiProfile()
    {
        CreateMap<CreateHeroiDto, Heroi>();
        CreateMap<UpdateHeroiDto, Heroi>();
        CreateMap<Heroi, ReadHeroiDto>()
            .ForMember(heroiDto => heroiDto.HeroiSuperPoderes,
                opt => opt.MapFrom(heroi => heroi.HeroiSuperPoderes));
    }
}
