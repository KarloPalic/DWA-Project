using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DwaMoviesContext _context;

        public GenreRepository(DwaMoviesContext context)
        {
            _context = context;
        }

        public void AddGenre(Genre genre)
        {
            if (genre == null)
                throw new ArgumentNullException(nameof(genre));

            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        public void DeleteGenre(int id)
        {
            var genre = _context.Genres.Find(id);

            if (genre == null)
                throw new InvalidOperationException($"Genre with ID {id} not found.");

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }

        public Genre GetGenreById(int id)
        {
            return _context.Genres.Find(id);
        }

        public Genre GetGenreByName(string name)
        {
            return _context.Genres
                .FirstOrDefault(g => g.Name == name);
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        public void UpdateGenre(Genre genre)
        {
            if (genre == null)
                throw new ArgumentNullException(nameof(genre));

            var existingGenre = _context.Genres.Find(genre.Id);

            if (existingGenre == null)
                throw new InvalidOperationException($"Genre with ID {genre.Id} not found.");

            existingGenre.Name = genre.Name;
            existingGenre.Description = genre.Description;

            _context.SaveChanges();
        }
    }
}
