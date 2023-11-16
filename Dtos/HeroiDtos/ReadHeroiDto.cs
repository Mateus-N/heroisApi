using HeroisApi.Dtos.HeroisSuperPoderesDtos;
using HeroisApi.Dtos.SuperPoderesDtos;

namespace HeroisApi.Dtos.HeroiDtos;

public class ReadHeroiDto
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string NomeHeroi { get; set; }
    public DateTime? DataNascimento { get; set; }
    public double Altura { get; set; }
    public double Peso { get; set; }
    public virtual required ICollection<ReadHeroisSuperPoderesDto> HeroiSuperPoderes { get; set; }
}
