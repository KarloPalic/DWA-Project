using AdministrativeModule.Models;
using DAL.DTO;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AdministrativeModule.Controllers
{
    public class HomeController : Controller
    {
        private readonly DwaMoviesContext _context;
        private readonly IVideoRepository _videoContext;
        private readonly IGenreRepository _genreRepository;
        private readonly IImageRepository _imageRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ITagRepository _tagRepository;
    
        public HomeController(DwaMoviesContext context, IVideoRepository videoContext, IGenreRepository genreRepository, IImageRepository imageRepository, ICountryRepository countryRepository, ITagRepository tagRepository)
        {
            _context = context;
            _videoContext = videoContext;
            _genreRepository = genreRepository;
            _imageRepository = imageRepository;
            _countryRepository = countryRepository;
            _tagRepository = tagRepository;
        }

        public IActionResult IndexAdmin(int page = 1, int pageSize = 10, string filterByName = "", string filterByGenre = "")
        {
            var videos = _videoContext.GetVideosByGenre(page, pageSize, filterByName, filterByGenre);

            var genres = _genreRepository.GetGenres();

            var images = _imageRepository.GetImages();

            var totalVideos = _videoContext.GetTotalVideos(filterByName, filterByGenre);

            var totalPages = (int)Math.Ceiling((double)totalVideos / pageSize);

            var model = new VideoList
            {
                Videos = videos,
                Genres = genres,
                Images = images,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                FilterByName = filterByName,
                FilterByGenre = filterByGenre
            };

            return View(model);
        }

        public IActionResult CreateVideo()
        {
            var genres = _genreRepository.GetGenres();

            var model = new VideoCreate
            {
                Genres = genres.ToList()
            };

            return View("CreateVideo", model);
        }

        [HttpPost]
        public IActionResult CreateVideo(VideoCreate videoCreate)
        {
            if (ModelState.IsValid)
            {
                var video = new Video
                {
                    Name = videoCreate.Name,
                    CreatedAt = DateTime.Now,
                    Description = videoCreate.Description,
                    GenreId = videoCreate.GenreId,
                    TotalSeconds = videoCreate.TotalSeconds,
                    StreamingUrl = videoCreate.StreamingUrl,
                    ImageId = videoCreate.ImageContentId
                };

                   _context.Videos.Add(video);

                   _context.SaveChanges();

                return RedirectToAction("IndexAdmin");
            }


            return View("CreateVideo", videoCreate);
        }

        public IActionResult SelectVideo(string? selectedVideoName)
        {
            if (!string.IsNullOrEmpty(selectedVideoName))
            {
                var video = _videoContext.GetVideoByName(selectedVideoName);
                var genres = _genreRepository.GetGenres();

                if (video != null)
                {
                    var model = new SelectVideo
                    {
                        VideoNames = _videoContext.GetVideoNames().ToList(),
                        SelectedVideoName = selectedVideoName,
                        Video = video,
                        Genres = genres
                    };

                    return View("SelectVideo", model);
                }
            }

            return View("SelectVideo", new SelectVideo
            {
                VideoNames = _videoContext.GetVideoNames().ToList(),
                SelectedVideoName = null,
                Video = null
            });
        }


        [HttpPut]
        public IActionResult SaveVideoChanges([FromBody] Video video)
        {
            if (video == null || video.Id <= 0)
            {
                return BadRequest("Invalid video data");
            }

            try
            {
                _videoContext.UpdateVideo(video);
                return Ok("Changes saved successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving changes: {ex.Message}");
            }
        }


        [HttpDelete]
        public IActionResult DeleteVideo([FromBody] int videoId)
        {
            if (videoId <= 0)
            {
                return BadRequest("Invalid video ID");
            }

            try
            {
                _videoContext.DeleteVideo(videoId);
                return Ok("Video deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting video: {ex.Message}");
            }
        }

        public IActionResult Countries(int page = 1, int pageSize = 10, string filterByCode = "")
        {
            var countries = _countryRepository.GetCountries(page, pageSize, filterByCode);

            var totalCountries = _countryRepository.GetTotalCountries(filterByCode);

            var totalPages = (int)Math.Ceiling((double)totalCountries / pageSize);

            var model = new CountryList
            {
                Countries = countries,
                Page = page,
                PageSize = pageSize,
                Totalpages = totalPages,
                FilterByCode = filterByCode
            };

            return View(model);
        }



    }
}