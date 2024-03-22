using DAL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AdministrativeModule.Models
{
    public class VideoCreate
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required]
        public int TotalSeconds { get; set; }

        [StringLength(256)]
        public string StreamingUrl { get; set; }

        [Required]

        public int ImageContentId { get; set; }

        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
