using DAL.DTO;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DWA_Project.Controllers
{
    [ApiController]
    [Route("api/videos")]
    //[Authorize]
    public class VideoController : ControllerBase
    {
        private readonly DwaMoviesContext _context;
        private readonly IVideoRepository _videoContext;

        public VideoController(DwaMoviesContext context, IVideoRepository videoContext)
        {
            _context = context;
            _videoContext = videoContext;
        }

        // GET api/videos
        //[AllowAnonymous]
        [HttpGet("withFilters")]
        public ActionResult<IEnumerable<VideoDTO>> GetVideos([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string filterByName = "", [FromQuery] string orderBy = "")
        {
            var videos = _videoContext.GetVideos(page, pageSize, filterByName, orderBy)
                .Select(v => new VideoDTO
                {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    GenreId = v.GenreId,
                    TotalSeconds = v.TotalSeconds,
                    StreamingUrl = v.StreamingUrl,
                    ImageId = v.ImageId,
                    TagIds = v.VideoTags.Select(vt => vt.TagId).ToList()
                })
                .ToList();

            return videos;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VideoDTO>> GetAllVideos()
        {
            var videos = _context.Videos
                .Select(v => new VideoDTO
                {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    GenreId = v.GenreId,
                    TotalSeconds = v.TotalSeconds,
                    StreamingUrl = v.StreamingUrl,
                    ImageId = v.ImageId,
                    TagIds = v.VideoTags.Select(vt => vt.TagId).ToList()
                })
                .ToList();

            return videos;
        }

        //[AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<VideoDTO> GetVideo(int id)
        {
            var video = _context.Videos
                .Where(v => v.Id == id)
                .Select(v => new VideoDTO
                {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    GenreId = v.GenreId,
                    TotalSeconds = v.TotalSeconds,
                    StreamingUrl = v.StreamingUrl,
                    ImageId = v.ImageId,
                    TagIds = v.VideoTags.Select(vt => vt.TagId).ToList()
                })
                .FirstOrDefault();

            if (video == null)
                return NotFound();

            return video;
        }

        [HttpPost]
        public ActionResult<VideoDTO> CreateVideo(VideoDTO videoDTO)
        {
        
            var video = new Video
            {
                Name = videoDTO.Name,
                Description = videoDTO.Description,
                GenreId = videoDTO.GenreId,
                TotalSeconds = videoDTO.TotalSeconds,
                StreamingUrl = videoDTO.StreamingUrl,
                ImageId = videoDTO.ImageId
            };

            foreach (var tagId in videoDTO.TagIds)
            {
                var videoTag = new VideoTag
                {
                    TagId = tagId,
                    Video = video
                };

                _context.VideoTags.Add(videoTag);
            }

            _context.Videos.Add(video);

            _context.SaveChanges();

            videoDTO.Id = video.Id;

            return CreatedAtAction(nameof(GetVideo), new { id = video.Id }, videoDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVideo(int id, [FromBody] VideoDTO videoDTO)
        {
            var existingVideo = _context.Videos.Find(id);

            if (existingVideo == null)
                return NotFound();

            existingVideo.Name = videoDTO.Name;
            existingVideo.Description = videoDTO.Description;
            existingVideo.GenreId = videoDTO.GenreId;
            existingVideo.TotalSeconds = videoDTO.TotalSeconds;
            existingVideo.StreamingUrl = videoDTO.StreamingUrl;
            existingVideo.ImageId = videoDTO.ImageId;

            _context.VideoTags.RemoveRange(_context.VideoTags.Where(vt => vt.VideoId == id));

            foreach (var tagId in videoDTO.TagIds)
            {
                var videoTag = new VideoTag
                {
                    TagId = tagId,
                    VideoId = id
                };

                _context.VideoTags.Add(videoTag);
            }

            _context.SaveChanges();

            return Ok(new { Message = "Video updated successfully" });
        }


        // DELETE api/videos/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteVideo(int id)
        {
            var video = _context.Videos.Find(id);

            if (video == null)
                return NotFound();

            _context.Videos.Remove(video);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
