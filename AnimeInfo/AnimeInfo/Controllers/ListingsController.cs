using Microsoft.AspNetCore.Mvc;

namespace AnimeInfo.Controllers
{
    public class ListingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
