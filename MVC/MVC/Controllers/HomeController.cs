using Microsoft.AspNetCore.Mvc;
using MVC.DTOs;
using MVC.Models;
using System.Diagnostics;

namespace MVC.Controllers
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
            var user = new UserDto
            {
                Name = "febri",
                Hobbies = new List<string> { "gaming" },
                LastName = "anto"
            };
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Users()
        {
            ViewData["Title"] = "User list";

            return View();
        }

        [HttpPost]
        public IActionResult Users([FromBody] UserDto userDto)
        {

            if (userDto != null)
            {
                foreach (var hobby in userDto.Hobbies)
                {
                    Console.WriteLine($"Hobbiy : {hobby}");
                }
            }
            return View();
        }


        public IActionResult Users2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Users2(UserDto2 userDto)
        {
            Console.WriteLine("user name : " + userDto.Name);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}