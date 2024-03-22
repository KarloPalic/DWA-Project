using DAL.Models;

namespace AdministrativeModule.Models
{
    public class CountryList
    {
        public IEnumerable<Country> Countries { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Totalpages { get; set; }
        public string FilterByCode { get; set; }
    }
}
