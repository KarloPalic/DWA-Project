using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DWA_Project.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly DwaMoviesContext _context;
        private readonly GenreRepository _genreRepository;

        public GenreController(DwaMoviesContext context, GenreRepository genreRepository)
        {
            _context = context;
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Genre>> GetGenres()
        {
            var genres = _context.Genres.ToList();
            return genres;
        }

        [HttpGet("{id}")]
        public ActionResult<Genre> GetGenre(int id)
        {
            var genre = _context.Genres.Find(id);

            if (genre == null)
                return NotFound();

            return genre;
        }

        [HttpGet("byName/{name}")]
        public ActionResult<Genre> GetGenreByName(string name)
        {
            var genre = _genreRepository.GetGenreByName(name);

            if (genre == null)
                return NotFound();

            return genre;
        }

        [HttpPost]
        public ActionResult<Genre> CreateGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, Genre genre)
        {
            if (id != genre.Id)
                return BadRequest();

            _context.Entry(genre).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            var genre = _context.Genres.Find(id);

            if (genre == null)
                return NotFound();

            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
