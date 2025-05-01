using Microsoft.AspNetCore.Mvc;

namespace LibProject.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard() => View();

    }
}