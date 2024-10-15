using Microsoft.AspNetCore.Mvc;

namespace AnimeInfo.Controllers
{
    public class Blog : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Links()
        {
            return View();
        }
    }
}
