using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentityApp.Controllers;

[Authorize]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        Console.WriteLine("User Id: " + User.FindFirstValue(ClaimTypes.NameIdentifier));
        Console.WriteLine("Username: " + User.FindFirstValue(ClaimTypes.Name));
        Console.WriteLine("Role: " + User.FindFirstValue(ClaimTypes.Role));
        Console.WriteLine("Country: " + User.FindFirstValue(ClaimTypes.Country));
        Console.WriteLine("Expired: " + User.FindFirstValue(ClaimTypes.Expired));
        Console.WriteLine("Email: " + User.FindFirstValue(ClaimTypes.Email));
        Console.WriteLine("MobilePhone: " + User.FindFirstValue(ClaimTypes.MobilePhone));
        Console.WriteLine("Expiration: " + User.FindFirstValue(ClaimTypes.Expiration));
        Console.WriteLine("DateofBith: " + User.FindFirstValue(ClaimTypes.DateOfBirth));
        Console.WriteLine("Gender: " + User.FindFirstValue(ClaimTypes.Gender));
        Console.WriteLine("Name: " + User.FindFirstValue("Name"));

        return View();
    }
}

