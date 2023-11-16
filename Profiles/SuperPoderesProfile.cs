using AutoMapper;
using HeroisApi.Dtos.SuperPoderesDtos;
using HeroisApi.Models;

namespace HeroisApi.Profiles;

public class SuperPoderesProfile : Profile
{
    public SuperPoderesProfile()
    {
        CreateMap<CreateSuperPoderesDto, SuperPoderes>();
        CreateMap<UpdateSuperPoderesDto, SuperPoderes>();
        CreateMap<SuperPoderes, ReadSuperPoderesDto>();
    }
}
