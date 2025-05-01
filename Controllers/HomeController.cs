using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibProject.DataBase;
using LibProject.Models.Domain;
using System.Security.Claims;

namespace LibProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Favorites)
                .AsNoTracking()
                .ToListAsync();

            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = GetCurrentUserId();
                foreach (var book in books)
                {
                    book.IsFavorite = book.Favorites?.Any(f => f.ReaderId == userId) ?? false;
                }
            }

            // Гарантированно не-null значение
            ViewBag.Genres = await _context.Genres.ToListAsync() ?? new List<Genre>();
            
            return View(books);
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userIdClaim ?? throw new InvalidOperationException("User not found"));
        }
    }
}