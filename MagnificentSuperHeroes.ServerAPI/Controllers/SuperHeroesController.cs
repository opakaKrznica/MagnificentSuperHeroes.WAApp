using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        // GET: api/SuperHeroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperHero>>> GetSuperHeroes()
        {
          if (_context.SuperHeroes == null)
          {
              return NotFound();
          }
            return await _context.SuperHeroes.ToListAsync();
        }

        // GET: api/SuperHeroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSuperHero(int id)
        {
          if (_context.SuperHeroes == null)
          {
              return NotFound();
          }
            var superHero = await _context.SuperHeroes.FindAsync(id);

            if (superHero == null)
            {
                return NotFound();
            }

            return superHero;
        }

        // PUT: api/SuperHeroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuperHero(int id, SuperHero superHero)
        {
            if (id != superHero.Id)
            {
                return BadRequest();
            }

            _context.Entry(superHero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuperHeroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SuperHeroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SuperHero>> PostSuperHero(SuperHero superHero)
        {
          if (_context.SuperHeroes == null)
          {
              return Problem("Entity set 'MagSuperHeroContext.SuperHeroes'  is null.");
          }
            _context.SuperHeroes.Add(superHero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSuperHero", new { id = superHero.Id }, superHero);
        }

        // DELETE: api/SuperHeroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperHero(int id)
        {
            if (_context.SuperHeroes == null)
            {
                return NotFound();
            }
            var superHero = await _context.SuperHeroes.FindAsync(id);
            if (superHero == null)
            {
                return NotFound();
            }

            _context.SuperHeroes.Remove(superHero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SuperHeroExists(int id)
        {
            return (_context.SuperHeroes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
