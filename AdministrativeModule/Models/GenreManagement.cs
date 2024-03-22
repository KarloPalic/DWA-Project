using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace AdministrativeModule.Models
{
    public class GenreManagement
    {
        public IEnumerable<Genre> Genres { get; set; }


        [Required]
        [StringLength(256)]

        public string Name { get; set; }

        [Required]
        [StringLength(1024)]

        public string Description { get; set; }
    }
}
