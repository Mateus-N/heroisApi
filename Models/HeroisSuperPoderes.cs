namespace HeroisApi.Models;

public record HeroisSuperPoderes
{
    public int HeroiId { get; set; }
    public virtual required Heroi Heroi { get; set; }
    public int SuperPoderesId { get; set; }
    public virtual required SuperPoderes SuperPoderes { get; set; }
}
