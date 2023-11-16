namespace HeroisApi.Services.Interfaces
{
    public interface IHeroisSuperPoderesService
    {
        Task ApagaPoderesDoHerois(int id);
        Task AtualizaPoderes(int heroiId, int[] poderesId);
        Task CadastraPoderes(int heroiId, int[] poderesId);
    }
}