using DAL.DataTransferObject;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using PublicModule.Models;
using System.Security.Cryptography;

namespace PublicModule.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly DwaMoviesContext _context;
        private readonly IVideoRepository _videoRepository;
        private readonly IImageRepository _imageRepository;

        public HomeController(DwaMoviesContext context, IUserRepository userRepository, IVideoRepository videoRepository, IImageRepository imageRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _videoRepository = videoRepository;
            _imageRepository = imageRepository;
        }
        public IActionResult Index(string filterByName = "")
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");

            var videos = _videoRepository.GetVideosByNames(filterByName);
            var images = _imageRepository.GetImages();

            var model = new CardView
            {
                Videos = videos,
                Images = images,
                FilterByName = filterByName,
            };

            return View(model);
        }

        public IActionResult CardSelection(int? videoId)
        {
            if (videoId.HasValue)
            {
                ViewBag.Username = HttpContext.Session.GetString("Username");

                var video = _videoRepository.GetVideoById(videoId.Value);

                var model = new CardView
                {
                    Videos = new List<Video> { video },
                    SelectedVideo = video
                };

                return View("CardSelection", model);
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginView model)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.GetUserByUsername(new User { Username = model.Username });

                if (user != null && ValidatePassword(model.Password, user.PwdSalt, user.PwdHash))
                {
                    HttpContext.Session.SetString("Username", user.Username);
                    TempData["ShowSuccessMessage"] = true;

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var registrationDto = new UserRegistrationDTO
                    {
                        Username = model.Username,
                        Password = model.Password,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Phone = model.Phone,
                        CountryOfResidenceName = model.CountryOfResidenceName,
                    };

                    var registrationResult = await _userRepository.RegisterUser(registrationDto);

                    if (registrationResult)
                    {
                        return RedirectToAction("Login");
                    }

                    ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred during registration: {ex.Message}");
                }
            }

            return View();
        }



        private bool ValidatePassword(string inputPassword, string salt, string storedHash)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(inputPassword, Convert.FromBase64String(salt), 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashBytes = new byte[36];
                Array.Copy(Convert.FromBase64String(salt), 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                var inputHash = Convert.ToBase64String(hashBytes);

                return inputHash == storedHash;
            }
        }
    }
}