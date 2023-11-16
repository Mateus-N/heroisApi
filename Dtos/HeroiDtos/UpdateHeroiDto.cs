using System.ComponentModel.DataAnnotations;

namespace HeroisApi.Dtos.HeroiDtos;

public class UpdateHeroiDto
{
    [StringLength(120, ErrorMessage = "o nome deve ter no máximo 120 caracteres")]
    [Required(ErrorMessage = "O campo de nome é obrigatório")]
    public required string Nome { get; set; }
    [StringLength(120, ErrorMessage = "o nomeHeroi deve ter no máximo 120 caracteres")]
    [Required(ErrorMessage = "O campo de nomeHeroi é obrigatório")]
    public required string NomeHeroi { get; set; }
    public required int[]? SuperPoderes { get; set; }
    public DateTime? DataNascimento { get; set; }
    [Required(ErrorMessage = "O campo de altura é obrigatório")]
    public double Altura { get; set; }
    [Required(ErrorMessage = "O campo de peso é obrigatório")]
    public double Peso { get; set; }
}
