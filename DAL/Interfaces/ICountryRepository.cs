using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetCountries();
        IEnumerable<Country> GetCountries(int page, int pageSize, string filterByCode);

        int GetTotalCountries(string filterByCode);
        Country GetCountryById(int id);
        void AddCountry(Country country);
        void UpdateCountry(Country country);
        void DeleteCountry(int id);
    }
}
