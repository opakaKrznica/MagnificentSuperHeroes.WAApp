using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MagnificentSuperHeroes.ServerAPI.MSHBase;

public partial class Difficulty
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<SuperHero> SuperHeroes { get; set; } = new List<SuperHero>();
}
