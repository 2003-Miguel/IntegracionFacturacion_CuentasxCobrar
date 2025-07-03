using Microsoft.AspNetCore.Mvc;

namespace FarmaciaCarolAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        public IActionResult Consulta()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
