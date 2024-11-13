﻿using AnimeInfo.Models;
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
            Quiz model = new Quiz();
            return View(model);
        }
        [HttpPost]
        public IActionResult Quiz(Quiz model)
        {
            //TODO: do something with the quiz answers
            return View();
        }

    }
}
