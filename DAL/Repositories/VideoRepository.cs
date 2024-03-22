using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly DwaMoviesContext _context;

        public VideoRepository(DwaMoviesContext context)
        {
            _context = context;
        }

        public void AddVideo(Video video)
        {
            if (video == null)
                throw new ArgumentNullException(nameof(video));

            _context.Videos.Add(video);
            _context.SaveChanges();
        }

        public void DeleteVideo(int id)
        {
            var video = _context.Videos.Find(id);

            if (video == null)
                throw new InvalidOperationException($"Video with ID {id} not found.");

            _context.Videos.Remove(video);
            _context.SaveChanges();
        }

        public int GetTotalVideos(string filterByName, string filterByGenre)
        {
            var query = _context.Videos.AsQueryable();

            if (!string.IsNullOrEmpty(filterByName))
            {
                query = query.Where(v => v.Name.Contains(filterByName));
            }

            if (!string.IsNullOrEmpty(filterByGenre))
            {
                query = query.Where(v => v.Genre.Name.Contains(filterByGenre));
            }

            return query.Count();
        }

        public Video GetVideoById(int videoId)
        {
            return _context.Videos
                    .Include(v => v.Genre)
                    .Include(v => v.Image)
        .FirstOrDefault(v => v.Id == videoId);
        }

        public Video GetVideoByName(string name)
        {
            return _context.Videos.FirstOrDefault(v => v.Name == name);
        }

        public IEnumerable<string> GetVideoNames()
        {
            return _context.Videos.Select(v => v.Name);
        }

        public IEnumerable<Video> GetVideos()
        {
            return _context.Videos.ToList();
        }

        public IEnumerable<Video> GetVideos(int page, int pageSize, string filterByName, string orderBy)
        {
            var query = _context.Videos.AsQueryable();

            if (!string.IsNullOrEmpty(filterByName))
            {
                query = query.Where(v => v.Name.Contains(filterByName));
            }

            switch (orderBy?.ToLower())
            {
                case "id":
                    query = query.OrderBy(v => v.Id);
                    break;
                case "name":
                    query = query.OrderBy(v => v.Name);
                    break;
                case "totaltime":
                    query = query.OrderBy(v => v.TotalSeconds);
                    break;
                default:
                    query = query.OrderBy(v => v.Id);
                    break;
            }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return query.ToList();
        }

        public IEnumerable<Video> GetVideosByGenre(int page, int pageSize, string filterByName, string genreName)
        {
            var query = _context.Videos.AsQueryable();

            if (!string.IsNullOrEmpty(filterByName))
            {
                query = query.Where(v => v.Name.Contains(filterByName));
            }

            if (!string.IsNullOrEmpty(genreName))
            {
                query = query.Where(v => v.Genre.Name.Contains(genreName));
            }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return query.ToList();
        }

        public IEnumerable<Video> GetVideosByNames(string filterByName)
        {
            var query = _context.Videos.AsQueryable();

            if (!string.IsNullOrEmpty(filterByName))
            {
                query = query.Where(v => v.Name.Contains(filterByName));
            }

            return query.ToList();
        }

        public void UpdateVideo(Video video)
        {
            if (video == null)
                throw new ArgumentNullException(nameof(video));

            var existingVideo = _context.Videos.Find(video.Id);

            if (existingVideo == null)
                throw new InvalidOperationException($"Video with ID {video.Id} not found.");

            existingVideo.Name = video.Name;
            existingVideo.Description = video.Description;
            existingVideo.GenreId = video.GenreId;
            existingVideo.TotalSeconds = video.TotalSeconds;
            existingVideo.StreamingUrl = video.StreamingUrl;
            existingVideo.ImageId = video.ImageId;

            _context.SaveChanges();
        }
    }
}
