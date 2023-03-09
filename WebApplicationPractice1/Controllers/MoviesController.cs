using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationPractice1.Models;

namespace WebApplicationPractice1.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesContext _dbContext;

        public MoviesController(MoviesContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movies>>> GetMovies() {
            
            if(_dbContext.Movies == null)
            {
                return NotFound();
            }
            return await _dbContext.Movies.ToListAsync();
        }


        //GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movies>> GetMovies(int id)
        {

            if (_dbContext.Movies == null)
            {
                return NotFound();
            }
            var movie =  await _dbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

        //POST: api/movies
        [HttpPost]
        public async Task<ActionResult<Movies>> PostMovies(Movies movie)
        {
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();

           return CreatedAtAction(nameof(GetMovies), new {id = movie.Id } ,movie);
        }

        //PUT; api/Movies/5
        [HttpPut]
        public async Task<ActionResult<Movies>> PutMovie(int id, Movies movie)
        {
            if(id != movie.Id)
                return BadRequest();
            _dbContext.Movies.Entry(movie).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                    return NotFound();
                else throw;
            }
            return NoContent();
        }

        private bool MovieExists(long id)
        {
            return (_dbContext.Movies?.Any(x => x.Id == id)).GetValueOrDefault();
        }


        //DELETE : api/Movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movies>> DeleteMovie(int id)
        {
            if(_dbContext.Movies == null)
                return NotFound();

            var movie = await _dbContext.Movies.FindAsync(id);
            if(movie == null) return NotFound();

            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
