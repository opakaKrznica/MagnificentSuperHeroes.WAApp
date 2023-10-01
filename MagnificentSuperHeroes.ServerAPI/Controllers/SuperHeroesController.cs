
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagnificentSuperHeroes.ServerAPI.MSHBase;

namespace MagnificentSuperHeroes.ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroesController : ControllerBase
    {
        private readonly MagSuperHeroContext _context;

        public SuperHeroesController(MagSuperHeroContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperHero>>> GetSuperHeroes()
        {
            var heroes = await _context.SuperHeroes
              .Include(h => h.Comic)
              .Include(h => h.Team)
              .Include(h => h.Difficulty)
              .ToListAsync();
           
            if (_context.SuperHeroes == null)
            {
                return NotFound();
            }
            return Ok(heroes);
            
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSuperHero(int id)
        {
            var hero = await _context.SuperHeroes
               .Include(h => h.Comic)
               .Include(h => h.Team)
               .Include(h => h.Difficulty)
               .FirstOrDefaultAsync(h => h.Id == id);
            if (hero == null)
            {
                return NotFound("Sorry, no heroes here. :/");
            }
            return Ok(hero);
        
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SuperHero>> PutSuperHero(int id, SuperHero hero)
        {
            var dbHero = await _context.SuperHeroes
                          .Include(h => h.Comic)
                         .Include(h => h.Team)
                         .Include(h => h.Difficulty)
                         .FirstOrDefaultAsync(h => h.Id == id);

            if (dbHero == null)
                return NotFound("Sorry, no Hero for you. :/");

            dbHero.Name = hero.Name;
            dbHero.HeroName = hero.HeroName;
            dbHero.Bio = hero.Bio;
            dbHero.BirthDate = hero.BirthDate;
            dbHero.TeamId = hero.TeamId;
            dbHero.ComicId = hero.ComicId;
            dbHero.DifficultyId = hero.DifficultyId;
            dbHero.IsReadyToFight = hero.IsReadyToFight;
            dbHero.Image = hero.Image;

            await _context.SaveChangesAsync();

            return Ok(await GetDbHeroes());
        }

        [HttpPost]
        public async Task<ActionResult<SuperHero>> PostSuperHero(SuperHero hero)
        {
            hero.Comic = null;
            hero.Team = null;
            hero.Difficulty = null;
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await GetDbHeroes());
            
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperHero(int id)
        {
            var dbHero = await _context.SuperHeroes
                  .Include(h => h.Comic)
                  .Include(h => h.Team)
                  .Include(h => h.Difficulty)
                  .FirstOrDefaultAsync(sh => sh.Id == id);

            if (dbHero == null)
                return NotFound("Sorry, no Hero for you. :/");

            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await GetDbHeroes());
          
        }

        private async Task<List<SuperHero>> GetDbHeroes()
        {
            return await _context.SuperHeroes
                .Include(sh => sh.Comic)
                .Include(h => h.Team)
                .Include(hx => hx.Difficulty)
                .ToListAsync();
        }   


        [HttpGet("comics")]
        public async Task<ActionResult<IEnumerable<Comic>>> GetComics()
        {
            if (_context.Comics == null)
            {
                return NotFound();
            }
            return await _context.Comics.ToListAsync();
        }

        [HttpGet("teams")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            if (_context.Teams == null)
            {
                return NotFound();
            }
            return await _context.Teams.ToListAsync();
        }

        [HttpGet("difficulties")]
        public async Task<ActionResult<IEnumerable<Difficulty>>> GetDifficulty()
        {
            if (_context.Difficulties == null)
            {
                return NotFound();
            }
            return await _context.Difficulties.ToListAsync();
        }
    }
}
