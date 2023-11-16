using HeroisApi.Data;
using Microsoft.EntityFrameworkCore;

namespace HeroisApi.Exceptions.Helper;

public class HeroiExceptionHelper : IHeroiExceptionHelper
{
    public async Task SaveChanges(AppDbContext context)
    {
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException!.Message.Contains("Duplicate"))
            {
                throw new DbException("Já existe outro Herói que utiliza esse nome");
            }
            if (e.InnerException!.Message.Contains("Data too long for column 'NomeHeroi'"))
            {
                throw new DbException("O campo de NomeHeroi deve no máximo 120 caracteres");
            }
            if (e.InnerException!.Message.Contains("Data too long for column 'Nome'"))
            {
                throw new DbException("O campo de Nome deve no máximo 120 caracteres");
            }
            throw new DbException("Erro não mapeado");
        }
    }
}
