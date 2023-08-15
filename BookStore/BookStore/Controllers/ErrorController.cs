using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

public class ErrorController : Controller
{
    public IActionResult Unauthorize()
    {
        return View();
    }

    public IActionResult NotFound()
    {
        return View();
    }

    public IActionResult Forbidden()
    {
        return View();
    }

    public IActionResult MethodNotAllowed()
    {
        return View();
    }

    public IActionResult InternalServerError()
    {
        return View();
    }
}

