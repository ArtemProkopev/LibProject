using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibProject.DataBase;
using LibProject.Models.Domain;
using LibProject.ViewModels.Account;
using System.Security.Cryptography;
using System.Text;

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
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Readers
                    .FirstOrDefaultAsync(r => r.FirstName == model.FirstName && r.LastName == model.LastName);

                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Пользователь с таким именем и фамилией уже существует");
                    return View(model);
                }

                var hashedPassword = HashPassword(model.Password);

                var reader = new Reader
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = hashedPassword
                };

                _context.Readers.Add(reader);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetInt32("UserId", reader.Id);
                HttpContext.Session.SetString("UserName", $"{reader.FirstName} {reader.LastName}");

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = HashPassword(model.Password);

                var reader = await _context.Readers
                    .FirstOrDefaultAsync(r => r.FirstName == model.FirstName &&
                                            r.LastName == model.LastName &&
                                            r.Password == hashedPassword);

                if (reader != null)
                {
                    HttpContext.Session.SetInt32("UserId", reader.Id);
                    HttpContext.Session.SetString("UserName", $"{reader.FirstName} {reader.LastName}");

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Неверное имя, фамилия или пароль");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}