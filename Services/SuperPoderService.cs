using AutoMapper;
using HeroisApi.Data;
using HeroisApi.Dtos.SuperPoderesDtos;
using HeroisApi.Exceptions;
using HeroisApi.Models;
using HeroisApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeroisApi.Services;

public class SuperPoderService(AppDbContext context, IMapper mapper) : ISuperPoderService
{
    private readonly AppDbContext context = context;
    private readonly IMapper mapper = mapper;

    public async Task<ReadSuperPoderesDto> CadastraSuperPoder(CreateSuperPoderesDto createDto)
    {
        SuperPoderes poder = mapper.Map<SuperPoderes>(createDto);
        await context.SuperPoderes.AddAsync(poder);
        await context.SaveChangesAsync();
        return mapper.Map<ReadSuperPoderesDto>(poder);
    }

    public IEnumerable<ReadSuperPoderesDto> BuscarTodos()
    {
        var readDto = mapper.Map<IEnumerable<ReadSuperPoderesDto>>(
            context.SuperPoderes.ToList());

        if (readDto.Count() == 0) throw new EmptyListException();

        return readDto;
    }

    public async Task<ReadSuperPoderesDto> BuscaUm(int id)
    {
        SuperPoderes? poder = await context.SuperPoderes
            .FirstOrDefaultAsync(s => s.Id == id);

        if (poder == null) throw new NotFoundException();

        return mapper.Map<ReadSuperPoderesDto>(poder);
    }

    public async Task<ReadSuperPoderesDto> AtualizaPoder(UpdateSuperPoderesDto updateDto)
    {
        SuperPoderes poder = mapper.Map<SuperPoderes>(updateDto);
        context.SuperPoderes.Update(poder);
        await context.SaveChangesAsync();
        return mapper.Map<ReadSuperPoderesDto>(poder);
    }

    public async Task ApagarPoder(int id)
    {
        SuperPoderes? poder = await context.SuperPoderes
            .FirstOrDefaultAsync(s => s.Id == id);

        if (poder == null) throw new NotFoundException();

        context.Remove(poder);
        await context.SaveChangesAsync();
    }
}
