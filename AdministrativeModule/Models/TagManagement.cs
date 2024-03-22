using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace AdministrativeModule.Models
{
    public class TagManagement
    {
        public IEnumerable<Tag> Tags { get; set; }


        [Required]
        [StringLength(256)]
        
        public string Name { get; set; }
    }
}
