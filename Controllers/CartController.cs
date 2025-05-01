// Controllers/CartController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibProject.DataBase;
using LibProject.Models.Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LibProject.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationContext _context;

        public CartController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId)
        {
            var userId = GetCurrentUserId();
            var book = await _context.Books.FindAsync(bookId);

            if (book == null)
                return NotFound("Книга не найдена");

            var existingItem = await _context.Baskets
                .FirstOrDefaultAsync(b => b.BookId == bookId && b.ReaderId == userId);

            if (existingItem == null)
            {
                var basketItem = new Basket
                {
                    BookId = bookId,
                    ReaderId = userId
                };

                _context.Baskets.Add(basketItem);
                await _context.SaveChangesAsync();
            }

            var cartItems = await GetCartItems(userId);
            return Json(cartItems);
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetCurrentUserId();
            return Json(await GetCartItems(userId));
        }

        private async Task<object> GetCartItems(int userId)
        {
            var items = await _context.Baskets
                .Include(b => b.Book)
                .Where(b => b.ReaderId == userId)
                .Select(b => new 
                {
                    id = b.Id,
                    title = b.Book.Title,
                    author = b.Book.Author,
                    price = b.Book.Price,
                    addedDate = b.AddedDate.ToString("dd.MM.yyyy HH:mm")
                })
                .ToListAsync();

            return new 
            {
                totalItems = items.Count,
                items = items,
                totalPrice = items.Sum(i => i.price)
            };
        }

        private int GetCurrentUserId()
				{
						var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
						return int.Parse(userIdClaim ?? throw new InvalidOperationException("User not found"));
				}
    }
}