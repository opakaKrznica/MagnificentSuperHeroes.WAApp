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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-JE9G202;Initial Catalog=MagSuperHero;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
