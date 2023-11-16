using HeroisApi.Data;

namespace HeroisApi.Exceptions.Helper
{
    public interface IHeroiExceptionHelper
    {
        Task SaveChanges(AppDbContext context);
    }
}