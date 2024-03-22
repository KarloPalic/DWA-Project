using DAL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdministrativeModule.Models
{
    public class SelectVideo
    {
        public string SelectedVideoName { get; set; }
        public IEnumerable<string> VideoNames { get; set; }
        public Video Video { get; set; } 
        public IEnumerable<Genre> Genres { get; set; }
    }
}
