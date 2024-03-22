using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class VideoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int GenreId { get; set; }
        public int TotalSeconds { get; set; }
        public string? StreamingUrl { get; set; }
        public int? ImageId { get; set; }
        public List<int> TagIds { get; set; } = new List<int>();
    }
}
