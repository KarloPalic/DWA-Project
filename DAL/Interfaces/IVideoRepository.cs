using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetVideos();
        IEnumerable<Video> GetVideos(int page, int pageSize, string filterByName, string orderBy);
        IEnumerable<Video> GetVideosByGenre(int page, int pageSize, string filterByName, string genreName);
        IEnumerable<Video> GetVideosByNames(string filterByName);

        int GetTotalVideos(string filterByName, string filterByGenre);
        Video GetVideoById(int id);
        Video GetVideoByName(string name);
        IEnumerable<string> GetVideoNames();
        void AddVideo(Video video);
        void UpdateVideo(Video video);
        void DeleteVideo(int id);
    }
}
