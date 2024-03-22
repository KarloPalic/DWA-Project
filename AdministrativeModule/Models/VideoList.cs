using DAL.Models;

namespace AdministrativeModule.Models
{
    public class VideoList
    {
        public IEnumerable<Video> Videos { get; set; }
        public IEnumerable<Genre> Genres { get; set; }

        public IEnumerable<Image> Images { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }

        public int TotalPages { get; set; }
        public string FilterByName { get; set; }
        public string FilterByGenre { get; set; }
    }
}
