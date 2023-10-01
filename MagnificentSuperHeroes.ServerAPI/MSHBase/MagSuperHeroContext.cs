using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MagnificentSuperHeroes.ServerAPI.MSHBase;

public partial class MagSuperHeroContext : DbContext
{
    public MagSuperHeroContext()
    {
    }

    public MagSuperHeroContext(DbContextOptions<MagSuperHeroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comic> Comics { get; set; }

    public virtual DbSet<Difficulty> Difficulties { get; set; }

    public virtual DbSet<SuperHero> SuperHeroes { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SuperHero>(entity =>
        {
            entity.HasIndex(e => e.ComicId, "IX_SuperHeroes_ComicId");

            entity.HasIndex(e => e.DifficultyId, "IX_SuperHeroes_DifficultyId");

            entity.HasIndex(e => e.TeamId, "IX_SuperHeroes_TeamId");

            entity.HasOne(d => d.Comic).WithMany(p => p.SuperHeroes).HasForeignKey(d => d.ComicId);

            entity.HasOne(d => d.Difficulty).WithMany(p => p.SuperHeroes).HasForeignKey(d => d.DifficultyId);

            entity.HasOne(d => d.Team).WithMany(p => p.SuperHeroes).HasForeignKey(d => d.TeamId);
        });

    }

}
