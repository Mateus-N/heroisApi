using AutoMapper;
using HeroisApi.Data;
using HeroisApi.Dtos.HeroiDtos;
using HeroisApi.Exceptions;
using HeroisApi.Exceptions.Helper;
using HeroisApi.Models;
using HeroisApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeroisApi.Services;

public class HeroiService(
    AppDbContext context,
    IMapper mapper,
    IHeroiExceptionHelper exceptionHelper,
    IHeroisSuperPoderesService heroisSuperPoderesService) : IHeroiService
{
    private readonly AppDbContext context = context;
    private readonly IMapper mapper = mapper;
    private readonly IHeroiExceptionHelper exceptionHelper = exceptionHelper;
    private readonly IHeroisSuperPoderesService heroisSuperPoderesService = heroisSuperPoderesService;

    public async Task<ReadHeroiDto> CadastraHeroi(CreateHeroiDto createDto)
    {
        Heroi heroi = mapper.Map<Heroi>(createDto);
        await context.Herois.AddAsync(heroi);
        await exceptionHelper.SaveChanges(context);
        await heroisSuperPoderesService.CadastraPoderes(heroi.Id, createDto.SuperPoderes);
        return mapper.Map<ReadHeroiDto>(heroi);
    }

    public IEnumerable<ReadHeroiDto> BuscarTodos()
    {
        var readDto = mapper.Map<IEnumerable<ReadHeroiDto>>(context.Herois.ToList());

        if (readDto.Count() == 0) throw new EmptyListException();

        return readDto;
    }

    public async Task<ReadHeroiDto> BuscaUm(int id)
    {
        Heroi heroi = await BuscaHeroiPorId(id);
        return mapper.Map<ReadHeroiDto>(heroi);
    }

    public async Task<ReadHeroiDto> AtualizaHeroi(int id, UpdateHeroiDto updateDto)
    {
        Heroi heroi = await BuscaHeroiPorId(id);
        mapper.Map(updateDto, heroi);
        context.Herois.Update(heroi);
        await exceptionHelper.SaveChanges(context);

        if (updateDto.SuperPoderes != null)
        {
            await heroisSuperPoderesService.AtualizaPoderes(heroi.Id, updateDto.SuperPoderes);
        }

        return mapper.Map<ReadHeroiDto>(heroi);
    }

    public async Task ApagarHeroi(int id)
    {
        Heroi? heroi = await BuscaHeroiPorId(id);

        await heroisSuperPoderesService.ApagaPoderesDoHerois(heroi.Id);

        context.Remove(heroi);
        await context.SaveChangesAsync();
    }

    private async Task<Heroi> BuscaHeroiPorId(int id)
    {
        Heroi? heroi = await context.Herois.FirstOrDefaultAsync(h => h.Id == id);

        if (heroi == null) throw new NotFoundException();

        return heroi;
    }
}
