using AnimeInfo.Models;
using AnimeInfo.AnimeQuiz;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnimeInfo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public  IActionResult Overview() 
        { 
            return View();
        }

        public IActionResult References()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Quiz()
        {
            QuizVM model = new QuizVM();
            return View(model);
        }

        [HttpPost]
        public IActionResult Quiz(string answer1, string answer2, string answer3)
        {
            QuizVM model = new QuizVM();

            model.Questions[0].UserA = answer1;
            model.Questions[0].IsRight = model.Questions[0].UserA == model.Questions[0].A;

            model.Questions[1].UserA = answer2;
            model.Questions[1].IsRight = model.Questions[1].UserA == model.Questions[1].A;

            model.Questions[2].UserA = answer3;
            model.Questions[2].IsRight = model.Questions[2].UserA == model.Questions[2].A;

            return View(model);
        }

    }
}
