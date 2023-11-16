using System.ComponentModel.DataAnnotations;

namespace HeroisApi.Dtos.SuperPoderesDtos;

public record UpdateSuperPoderesDto
{
    public int Id { get; set; }
    [StringLength(50, ErrorMessage = "o superPoder deve ter no máximo 50 caracteres")]
    [Required(ErrorMessage = "O campo de superpoder é obrigatório")]
    public required string SuperPoder { get; set; }
    [StringLength(250, ErrorMessage = "a descrição deve ter no máximo 250 caracteres")]
    public string? Descricao { get; set; }
}
