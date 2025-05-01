using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibProject.DataBase;
using LibProject.Models.Domain;
using LibProject.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LibProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var booksQuery = _context.Books
                    .Include(b => b.Genre)
                    .Include(b => b.Favorites)
                    .AsQueryable();

                if (User.Identity?.IsAuthenticated ?? false)
                {
                    var userId = GetCurrentUserId();
                    booksQuery = booksQuery.Select(b => new Book
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = b.Author,
                        Year = b.Year,
                        Price = b.Price,
                        ImageUrl = b.ImageUrl,
                        IsAvailable = b.IsAvailable,
                        Genre = b.Genre,
                        GenreId = b.GenreId,
                        Favorites = b.Favorites,
                        IsFavorite = b.Favorites.Any(f => f.ReaderId == userId)
                    });
                }
                else
                {
                    booksQuery = booksQuery.Select(b => new Book
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = b.Author,
                        Year = b.Year,
                        Price = b.Price,
                        ImageUrl = b.ImageUrl,
                        IsAvailable = b.IsAvailable,
                        Genre = b.Genre,
                        GenreId = b.GenreId,
                        Favorites = b.Favorites,
                        IsFavorite = false
                    });
                }

                var books = await booksQuery.AsNoTracking().ToListAsync();
                ViewBag.Genres = await _context.Genres.AsNoTracking().ToListAsync();

                return View(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при загрузке главной страницы");
                return View("Error", new ErrorViewModel 
                { 
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
                });
            }
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new InvalidOperationException("User ID not found in claims");
            }
            return int.Parse(userIdClaim);
        }
    }
}