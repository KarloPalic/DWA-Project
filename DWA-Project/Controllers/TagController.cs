using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DWA_Project.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly DwaMoviesContext _context;

        public TagController(DwaMoviesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tag>> GetTags()
        {
            var tags = _context.Tags.ToList();
            return tags;
        }

        [HttpGet("{id}")]
        public ActionResult<Tag> GetTag(int id)
        {
            var tag = _context.Tags.Find(id);

            if (tag == null)
                return NotFound();

            return tag;
        }

        [HttpPost]
        public ActionResult<Tag> CreateTag(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTag(int id, Tag tag)
        {
            if (id != tag.Id)
                return BadRequest();

            _context.Entry(tag).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTag(int id)
        {
            var tag = _context.Tags.Find(id);

            if (tag == null)
                return NotFound();

            _context.Tags.Remove(tag);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
