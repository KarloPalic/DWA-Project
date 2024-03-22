using DAL.Models;

namespace PublicModule.Models
{
    public class CardView
    {
        public IEnumerable<Video> Videos { get; set; }
        public IEnumerable<Image> Images { get; set; }

        public string FilterByName { get; set; }

        public Video SelectedVideo { get; set; }

    }
}
