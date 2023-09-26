using MagnificentSuperHeroes.WAApp.Pages;

namespace MagnificentSuperHeroes.WAApp.SuperHeroesService
{
    public interface ISuperHeroesService
    {
        SuperHero Hero { get; set; }
        List<SuperHero> SuperHeroes { get; set; }

        List<Comic> Comics { get; set; }

        List<Team> Teams { get; set; }

        List<Difficulty> Difficulties { get; set; }

        Task GetComic();

        Task GetTeam();

        Task GetDifficulty();

        Task GetSuperHeroes();

        Task<SuperHero> GetSingleHero(int id);

        Task CreateSuperHero(SuperHero hero);

        Task SetSuperHeroes(HttpResponseMessage result);

        Task DeleteSuperHero(int id);

        Task UpdateSuperHero(SuperHero hero);

        //Task OnFileChange(InputFileChangeEventArgs e);

    }
}
