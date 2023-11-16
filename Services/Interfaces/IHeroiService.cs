using HeroisApi.Dtos.HeroiDtos;

namespace HeroisApi.Services.Interfaces
{
    public interface IHeroiService
    {
        Task ApagarHeroi(int id);
        Task<ReadHeroiDto> AtualizaHeroi(int id, UpdateHeroiDto updateDto);
        IEnumerable<ReadHeroiDto> BuscarTodos();
        Task<ReadHeroiDto> BuscaUm(int id);
        Task<ReadHeroiDto> CadastraHeroi(CreateHeroiDto createDto);
    }
}