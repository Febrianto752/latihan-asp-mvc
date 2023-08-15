using IdentityApp.Models.Dtos;
using IdentityApp.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers;

public class UserAuthenticationController : Controller
{
    private readonly IUserAuthenticationService _service;

    public UserAuthenticationController(IUserAuthenticationService service)
    {
        _service = service;
    }

    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registration(RegistrationDto model)
    {
        if (!ModelState.IsValid)
        {

            return View();
        }

        model.Role = "User";
        var result = await _service.RegistrationAsync(model);

        if (result.StatusCode == 1)
        {
            TempData["Success"] = result.Message;
            return RedirectToAction("Registration");
        }
        else
        {
            TempData["Error"] = result.Message;
            return RedirectToAction("Registration");
        }
    }
    public IActionResult Login()
    {
        if (HttpContext.User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var result = await _service.LoginAsync(model);

        if (result.StatusCode == 1)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        else
        {
            TempData["Error"] = result.Message;
            return RedirectToAction("Login");
        }

    }

    public async Task<IActionResult> Logout()
    {
        await _service.LogoutAsync();
        return RedirectToAction("Login");
    }

    public async Task<IActionResult> CreateAdmin()
    {
        var model = new RegistrationDto
        {
            Username = "febri",
            Name = "febrianto",
            Email = "optimusprime200039@gmail.com",
            Password = "!Admin123"
        };

        model.Role = "Admin";
        var result = await _service.RegistrationAsync(model);
        return Ok(result);

    }

}

