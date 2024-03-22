using AdministrativeModule.Models;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdministrativeModule.Controllers
{
    public class GenreController : Controller
    {
        private readonly DwaMoviesContext _context;
        private readonly IGenreRepository _genreRepository;
        public GenreController(DwaMoviesContext context,  IGenreRepository genreRepository)
        {
            _context = context;
            _genreRepository = genreRepository;
        }
        public IActionResult CRUDGenre()
        {
            var genres = _genreRepository.GetGenres();

            var model = new GenreManagement
            {
                Genres = genres
            };

            return View(model);
        }

        public IActionResult CreateGenre()
        {
            return View();
        }



        [HttpPut]
        public IActionResult UpdateGenre([FromBody] Genre genre)
        {
            if (genre == null || genre.Id <= 0)
            {
                return BadRequest("Invalid genre data");
            }

            try
            {
                _genreRepository.UpdateGenre(genre);
                return Ok("Changes saved successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving changes: {ex.Message}");
            }
        }

        [HttpDelete]
        public IActionResult DeleteGenre([FromBody] int genreId)
        {
            if (genreId <= 0)
            {
                return BadRequest("Invalid genre ID");
            }

            try
            {
                var genreToDelete = _context.Genres.Find(genreId);

                if (genreToDelete == null)
                {
                    return NotFound("Genre not found");
                }

                var replacementGenre = _context.Genres.FirstOrDefault(g => g.Id != genreId);

                var videosToUpdate = _context.Videos.Where(v => v.GenreId == genreId);
                foreach (var video in videosToUpdate)
                {
                    video.GenreId = replacementGenre?.Id ?? 0;
                }

                _context.Genres.Remove(genreToDelete);

                _context.SaveChanges();

                return Ok("Genre deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting genre: {ex.Message}");
            }
        }




        [HttpPost]
        public IActionResult CreateGenre(Genre genreCreate)
        {
            if (ModelState.IsValid)
            {
                var genre = new Genre
                {
                    Name = genreCreate.Name,
                    Description = genreCreate.Description
                };

                _context.Genres.Add(genre);

                _context.SaveChanges();

                return RedirectToAction("CRUDGenre");
            }


            return View("CreateGenre", genreCreate);
        }
    }
}
