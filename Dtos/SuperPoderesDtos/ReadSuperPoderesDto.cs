namespace HeroisApi.Dtos.SuperPoderesDtos;

public record ReadSuperPoderesDto
{
    public int Id { get; set; }
    public required string SuperPoder { get; set; }
    public string? Descricao { get; set; }
}
