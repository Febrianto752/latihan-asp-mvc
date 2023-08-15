using BookStore.Data;
using BookStore.Dtos.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Controllers;

public class AuthController : Controller
{
    private readonly BookStoreDbContext _dbContext;
    public AuthController(BookStoreDbContext dbcontext)
    {
        _dbContext = dbcontext;
    }
    public IActionResult SignIn()
    {
        ClaimsPrincipal claimUser = HttpContext.User;

        if (claimUser!.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Index", "Book");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn([FromForm] SignInDto signIn)
    {
        var accountByEmail = _dbContext.Accounts.FirstOrDefault(a => a.Email == signIn.Email);

        if (ModelState.IsValid)
        {
            if (accountByEmail == null)
            {
                TempData["Error"] = "email or password wrong!";
                return RedirectToAction("SignIn");
            }

            var matchedPassword = accountByEmail.Password == signIn.Password;

            if (matchedPassword == false)
            {
                TempData["Error"] = "email or password wrong!";
                return RedirectToAction("SignIn");
            }

            var accountWithRoles = (from acc in _dbContext.Accounts
                                    join ar in _dbContext.AccountRoles
                                    on acc.Guid equals ar.AccountGuid
                                    join role in _dbContext.Roles
                                    on ar.RoleGuid equals role.Guid
                                    where acc.Email == accountByEmail.Email
                                    select new SignedDto
                                    {
                                        Email = acc.Email,
                                        RoleName = role.Name
                                    }).ToList()[0];

            List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email, accountWithRoles.Email),
                        new Claim(ClaimTypes.Role, accountWithRoles.RoleName)
                    };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);



            return RedirectToAction("Index", "Book");
        }


        return View();
    }

    public async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("SignIn", "Auth");
    }


}

