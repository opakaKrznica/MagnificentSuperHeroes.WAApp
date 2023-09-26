using System;
using System.Collections.Generic;

namespace MagnificentSuperHeroes.ServerAPI.MSHBase;

public partial class SuperHero
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string HeroName { get; set; } = null!;

    public string Bio { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string Image { get; set; } = null!;

    public int ComicId { get; set; }

    public int TeamId { get; set; }

    public int DifficultyId { get; set; }

    public bool IsReadyToFight { get; set; }

    public virtual Comic Comic { get; set; } = null!;

    public virtual Difficulty Difficulty { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
