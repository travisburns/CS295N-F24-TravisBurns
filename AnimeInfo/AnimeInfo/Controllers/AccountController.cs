using Microsoft.AspNetCore.Mvc;

namespace AnimeInfo.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
