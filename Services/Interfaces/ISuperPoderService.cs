using HeroisApi.Dtos.SuperPoderesDtos;

namespace HeroisApi.Services.Interfaces
{
    public interface ISuperPoderService
    {
        Task ApagarPoder(int id);
        Task<ReadSuperPoderesDto> AtualizaPoder(UpdateSuperPoderesDto updateDto);
        IEnumerable<ReadSuperPoderesDto> BuscarTodos();
        Task<ReadSuperPoderesDto> BuscaUm(int id);
        Task<ReadSuperPoderesDto> CadastraSuperPoder(CreateSuperPoderesDto createDto);
    }
}