using AutoMapper;
using HeroisApi.Dtos.HeroisSuperPoderesDtos;
using HeroisApi.Models;

namespace HeroisApi.Profiles;

public class HeroiSuperPoderesProfile : Profile
{
    public HeroiSuperPoderesProfile()
    {
        CreateMap<CreateHeroisSuperPoderesDto, HeroisSuperPoderes>();
        CreateMap<HeroisSuperPoderes, ReadHeroisSuperPoderesDto>();
    }
}
