using AdministrativeModule.Models;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdministrativeModule.Controllers
{
    public class TagController : Controller
    {
        private readonly DwaMoviesContext _context;
        private readonly ITagRepository _tagRepository;

        public TagController(DwaMoviesContext context, ITagRepository tagRepository)
        {
            _context = context;
            _tagRepository = tagRepository;
        }
        public IActionResult CRUDTag()
        {
            var tags = _tagRepository.GetTags();

            var model = new TagManagement
            {
                Tags = tags
            };

            return View(model);
        }

        public IActionResult CreateTag()
        {
            return View();
        }



        [HttpPut]
        public IActionResult UpdateTag([FromBody] Tag tag)
        {
            if (tag == null || tag.Id <= 0)
            {
                return BadRequest("Invalid video data");
            }

            try
            {
                _tagRepository.UpdateTag(tag);
                return Ok("Changes saved successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving changes: {ex.Message}");
            }
        }


        [HttpDelete]
        public IActionResult DeleteTag([FromBody] int tagId)
        {
            if (tagId <= 0)
            {
                return BadRequest("Invalid tag ID");
            }

            try
            {
                _tagRepository.DeleteTag(tagId);
                return Ok("Tag deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting tag: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateTag(Tag tagCreate)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag
                {
                    Name = tagCreate.Name
                };

                _context.Tags.Add(tag);

                _context.SaveChanges();

                return RedirectToAction("CRUDTag");
            }


            return View("CreateTag", tagCreate);
        }
    }
}
