using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly DwaMoviesContext _context;

        public ImageRepository(DwaMoviesContext context)
        {
            _context = context;
        }

        public void AddImage(Image image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            _context.Images.Add(image);
            _context.SaveChanges();
        }

        public void DeleteImage(int id)
        {
            var image = _context.Images.Find(id);

            if (image == null)
                throw new InvalidOperationException($"Image with ID {id} not found.");

            _context.Images.Remove(image);
            _context.SaveChanges();
        }

        public Image GetImageById(int id)
        {
            return _context.Images.Find(id);
        }

        public IEnumerable<Image> GetImages()
        {
            return _context.Images.ToList();
        }

        public void UpdateImage(Image image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var existingImage = _context.Images.Find(image.Id);

            if (existingImage == null)
                throw new InvalidOperationException($"Image with ID {image.Id} not found.");

            existingImage.Content = image.Content;

            _context.SaveChanges();
        }
    }
}
