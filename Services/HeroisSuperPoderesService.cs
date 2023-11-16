using AutoMapper;
using HeroisApi.Data;
using HeroisApi.Dtos.HeroisSuperPoderesDtos;
using HeroisApi.Models;
using HeroisApi.Services.Interfaces;

namespace HeroisApi.Services;

public class HeroisSuperPoderesService(AppDbContext context, IMapper mapper) : IHeroisSuperPoderesService
{
    private readonly AppDbContext context = context;
    private readonly IMapper mapper = mapper;

    public async Task CadastraPoderes(int heroiId, int[] poderesId)
    {
        foreach (var poderId in poderesId)
        {
            CreateHeroisSuperPoderesDto dto = new(heroiId, poderId);
            HeroisSuperPoderes heroisSuper = mapper.Map<HeroisSuperPoderes>(dto);
            await context.HeroisSuperPoderes.AddAsync(heroisSuper);
        }
        await context.SaveChangesAsync();
    }

    public async Task AtualizaPoderes(int heroiId, int[] poderesId)
    {
        await AdicionaNovosPoderes(heroiId, poderesId);
        await RemovePoderesRetirados(heroiId, poderesId);
    }

    public async Task ApagaPoderesDoHerois(int id)
    {
        IEnumerable<HeroisSuperPoderes> poderesDoHeroi = context.HeroisSuperPoderes
                    .Where(p => p.HeroiId == id);

        foreach (var poder in poderesDoHeroi)
        {
            context.Remove(poder);
        }
        await context.SaveChangesAsync();
    }

    private async Task RemovePoderesRetirados(int heroiId, int[] poderesId)
    {
        IEnumerable<HeroisSuperPoderes> poderesCadastradosDoHeroi = context.HeroisSuperPoderes
                    .Where(p => p.HeroiId == heroiId);

        foreach (var poder in poderesCadastradosDoHeroi)
        {
            if (!poderesId.Contains(poder.SuperPoderesId))
            {
                context.Remove(poder);
            }
        }
        await context.SaveChangesAsync();
    }

    private async Task AdicionaNovosPoderes(int heroiId, int[] poderesId)
    {
        foreach (var poderId in poderesId)
        {
            HeroisSuperPoderes? poderEHeroiExiste = context.HeroisSuperPoderes
                .FirstOrDefault(p => p.SuperPoderesId == poderId && p.HeroiId == heroiId);

            if (poderEHeroiExiste == null)
            {
                CreateHeroisSuperPoderesDto dto = new(heroiId, poderId);
                HeroisSuperPoderes heroisSuper = mapper.Map<HeroisSuperPoderes>(dto);
                await context.HeroisSuperPoderes.AddAsync(heroisSuper);
            }
        }
        await context.SaveChangesAsync();
    }
}
