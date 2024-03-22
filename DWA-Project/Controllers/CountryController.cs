using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DWA_Project.Controllers
{
    public class CountryController : ControllerBase
    {
        private readonly DwaMoviesContext _context;
        private readonly CountryRepository _countryRepository;

        public CountryController(DwaMoviesContext context, CountryRepository countryRepository)
        {
            _context = context;
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Country>> GetCountries()
        {
            var countries = _context.Countries.ToList();
            return countries;
        }

        [HttpGet("{id}")]
        public ActionResult<Country> GetCountry(int id)
        {
            var country = _context.Countries.Find(id);

            if (country == null)
                return NotFound();

            return country;
        }


        [HttpPost]
        public ActionResult<Country> CreateCountry(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCountries), new { id = country.Id }, country);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, Country country)
        {
            if (id != country.Id)
                return BadRequest();

            _context.Entry(country).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            var country = _context.Countries.Find(id);

            if (country == null)
                return NotFound();

            _context.Countries.Remove(country);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
