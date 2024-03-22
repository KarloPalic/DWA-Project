using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class VideoTagRepository : IVideoTagRepository
    {
        private readonly DwaMoviesContext _context;

        public VideoTagRepository(DwaMoviesContext context)
        {
            _context = context;
        }

        public void AddVideoTag(VideoTag videoTag)
        {
            if (videoTag == null)
                throw new ArgumentNullException(nameof(videoTag));

            _context.VideoTags.Add(videoTag);
            _context.SaveChanges();
        }

        public void AddTagsToVideo(int videoId, IEnumerable<int> tagIds)
        {
            var video = _context.Videos.Find(videoId);

            if (video == null)
                throw new InvalidOperationException($"Video with ID {videoId} not found.");

            foreach (var tagId in tagIds)
            {
                var tag = _context.Tags.Find(tagId);

                if (tag == null)
                    throw new InvalidOperationException($"Tag with ID {tagId} not found.");

                var videoTag = new VideoTag
                {
                    VideoId = videoId,
                    TagId = tagId
                };

                _context.VideoTags.Add(videoTag);
            }

            _context.SaveChanges();
        }

        public void DeleteVideoTag(int id)
        {
            var videoTag = _context.VideoTags.Find(id);

            if (videoTag == null)
                throw new InvalidOperationException($"VideoTag with ID {id} not found.");

            _context.VideoTags.Remove(videoTag);
            _context.SaveChanges();
        }

        public VideoTag GetVideoTagById(int id)
        {
            return _context.VideoTags.Find(id);
        }

        public IEnumerable<VideoTag> GetVideoTags()
        {
            return _context.VideoTags.ToList();
        }

        public void UpdateVideoTag(VideoTag videoTag)
        {
            if (videoTag == null)
                throw new ArgumentNullException(nameof(videoTag));

            var existingVideoTag = _context.VideoTags.Find(videoTag.Id);

            if (existingVideoTag == null)
                throw new InvalidOperationException($"VideoTag with ID {videoTag.Id} not found.");

            existingVideoTag.VideoId = videoTag.VideoId;
            existingVideoTag.TagId = videoTag.TagId;

            _context.SaveChanges();
        }
    }
}
