using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DwaMoviesContext _context;

        public TagRepository(DwaMoviesContext context)
        {
            _context = context;
        }

        public void AddTag(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));

            _context.Tags.Add(tag);
            _context.SaveChanges();
        }

        public void DeleteTag(int id)
        {
            var tag = _context.Tags.Find(id);

            if (tag == null)
                throw new InvalidOperationException($"Tag with ID {id} not found.");

            _context.Tags.Remove(tag);
            _context.SaveChanges();
        }

        public Tag GetTagById(int id)
        {
            return _context.Tags.Find(id);
        }

        public IEnumerable<Tag> GetTags()
        {
            return _context.Tags.ToList();
        }

        public void UpdateTag(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));

            var existingTag = _context.Tags.Find(tag.Id);

            if (existingTag == null)
                throw new InvalidOperationException($"Tag with ID {tag.Id} not found.");

            existingTag.Name = tag.Name;

            _context.SaveChanges();
        }
    }
}
