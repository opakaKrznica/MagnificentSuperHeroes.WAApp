using System;
using System.Collections.Generic;

namespace MagnificentSuperHeroes.ServerAPI.MSHBase;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SuperHero> SuperHeroes { get; set; } = new List<SuperHero>();
}
