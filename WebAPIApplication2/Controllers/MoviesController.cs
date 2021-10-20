using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIApplication2.Models;

namespace WebAPIApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movies>>> GetMovieItems()
        {
            return await _context.MovieItems.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movies>> GetMovies(int id)
        {
            var movies = await _context.MovieItems.FindAsync(id);

            if (movies == null)
            {
                return NotFound();
            }

            return movies;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovies(int id, Movies movies)
        {
            if (id != movies.ID)
            {
                return BadRequest();
            }

            _context.Entry(movies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movies>> PostMovies(Movies movies)
        {
            _context.MovieItems.Add(movies);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovieItems), new { id = movies.ID }, movies);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovies(int id)
        {
            var movies = await _context.MovieItems.FindAsync(id);
            if (movies == null)
            {
                return NotFound();
            }

            _context.MovieItems.Remove(movies);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoviesExists(int id)
        {
            return _context.MovieItems.Any(e => e.ID == id);
        }
    }
}
