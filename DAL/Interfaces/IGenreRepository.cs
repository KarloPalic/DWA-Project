using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
        Genre GetGenreById(int id);
        Genre GetGenreByName(string genre);
        void AddGenre(Genre genre);
        void UpdateGenre(Genre genre);
        void DeleteGenre(int id);
    }
}
