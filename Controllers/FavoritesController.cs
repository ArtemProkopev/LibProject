// Controllers/FavoritesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibProject.DataBase;
using LibProject.Models.Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LibProject.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly ApplicationContext _context;

        public FavoritesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFavorite(int bookId)
        {
            var userId = GetCurrentUserId();
            
            var existing = await _context.Favorites
                .FirstOrDefaultAsync(f => f.BookId == bookId && f.ReaderId == userId);

            if (existing != null)
            {
                _context.Favorites.Remove(existing);
            }
            else
            {
                _context.Favorites.Add(new Favorites 
                { 
                    BookId = bookId, 
                    ReaderId = userId 
                });
            }

            await _context.SaveChangesAsync();
            return Ok(new { isFavorite = existing == null });
        }

        private int GetCurrentUserId()
				{
						var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
						return int.Parse(userIdClaim ?? throw new InvalidOperationException("User not found"));
				}
    }
}