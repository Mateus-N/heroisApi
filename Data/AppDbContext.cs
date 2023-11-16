using HeroisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroisApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> opts) : DbContext(opts)
{
    public DbSet<Heroi> Herois { get; set; }
    public DbSet<SuperPoderes> SuperPoderes { get; set; }
    public DbSet<HeroisSuperPoderes> HeroisSuperPoderes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Heroi>()
            .HasIndex(h => h.NomeHeroi)
            .IsUnique();

        builder.Entity<HeroisSuperPoderes>()
            .HasKey(p => new { p.SuperPoderesId, p.HeroiId });

        builder.Entity<HeroisSuperPoderes>()
            .HasOne(p => p.Heroi)
            .WithMany(h => h.HeroiSuperPoderes)
            .HasForeignKey(p => p.HeroiId);

        builder.Entity<HeroisSuperPoderes>()
            .HasOne(p => p.SuperPoderes)
            .WithMany(p => p.HeroiSuperPoderes)
            .HasForeignKey(p => p.SuperPoderesId);
    }
}
