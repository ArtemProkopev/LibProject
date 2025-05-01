using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using LibProject.DataBase;
using LibProject.Models.Domain;
using LibProject.Models;

namespace LibProject.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly ApplicationContext _context;

        public AdminController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            return View(new AdminDashboardViewModel
            {
                Readers = _context.Readers.ToList(),
                BorrowedBooks = _context.BorrowedBooks
                    .Include(bb => bb.Book)
                    .Include(bb => bb.Reader)
                    .ToList(),
                AllBooks = _context.Books
                    .Include(b => b.Genre)
                    .ToList()
            });
        }
    }
}