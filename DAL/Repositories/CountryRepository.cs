using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DwaMoviesContext _context;

        public CountryRepository(DwaMoviesContext context)
        {
            _context = context;
        }

        public void AddCountry(Country country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));

            _context.Countries.Add(country);
            _context.SaveChanges();
        }

        public void DeleteCountry(int id)
        {
            var country = _context.Countries.Find(id);

            if (country == null)
                throw new InvalidOperationException($"Country with ID {id} not found.");

            _context.Countries.Remove(country);
            _context.SaveChanges();
        }

        public IEnumerable<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public IEnumerable<Country> GetCountries(int page, int pageSize, string filterByCode)
        {
            var query = _context.Countries.AsQueryable();

            if (!string.IsNullOrEmpty(filterByCode))
            {
                query = query.Where(v => v.Code.Contains(filterByCode));
            }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return query.ToList();
        }

        public Country GetCountryById(int id)
        {
            return _context.Countries.Find(id);
        }

        public int GetTotalCountries(string filterByCode)
        {
            var query = _context.Countries.AsQueryable();

            if (!string.IsNullOrEmpty(filterByCode))
            {
                query = query.Where(v => v.Code.Contains(filterByCode));
            }

            return query.Count();
        }

        public void UpdateCountry(Country country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));

            var existingCountry = _context.Countries.Find(country.Id);

            if (existingCountry == null)
                throw new InvalidOperationException($"Country with ID {country.Id} not found.");

            existingCountry.Code = country.Code;
            existingCountry.Name = country.Name;

            _context.SaveChanges();
        }
    }
}
