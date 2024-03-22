using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IImageRepository
    {
        IEnumerable<Image> GetImages();
        Image GetImageById(int id);
        void AddImage(Image image);
        void UpdateImage(Image image);
        void DeleteImage(int id);
    }
}
