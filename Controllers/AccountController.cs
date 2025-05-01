using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibProject.DataBase;
using LibProject.Models.Domain;
using LibProject.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using LibProject.Utils;

namespace LibProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;

        public AccountController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (await _context.Readers.AnyAsync(r => 
                r.FirstName == model.FirstName && 
                r.LastName == model.LastName))
            {
                ModelState.AddModelError(string.Empty, "Пользователь уже существует");
                return View(model);
            }

            var reader = new Reader
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = PasswordHasher.HashPassword(model.Password),
                Role = "User"
            };

            _context.Readers.Add(reader);
            await _context.SaveChangesAsync();
            await SignInUser(reader);
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null, bool requireAdmin = false)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.RequireAdmin = requireAdmin;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(model);

            var hashedPassword = PasswordHasher.HashPassword(model.Password);
            var reader = await _context.Readers.FirstOrDefaultAsync(r => 
                r.FirstName == model.FirstName && 
                r.LastName == model.LastName && 
                r.Password == hashedPassword);

            if (reader == null)
            {
                ModelState.AddModelError(string.Empty, "Неверные данные");
                return View(model);
            }

            // Проверка прав администратора если требуется
            if (!string.IsNullOrEmpty(returnUrl) && returnUrl.Contains("/Admin") && reader.Role != "Admin")
            {
                ModelState.AddModelError(string.Empty, "Требуются права администратора");
                ViewBag.RequireAdmin = true;
                return View(model);
            }

            await SignInUser(reader);
            return LocalRedirect(returnUrl ?? "/Home/Index");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(Reader reader)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, reader.Id.ToString()),
                new(ClaimTypes.Name, $"{reader.FirstName} {reader.LastName}"),
                new(ClaimTypes.Role, reader.Role)
            };

            var identity = new ClaimsIdentity(
                claims, 
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                });
        }
    }
}