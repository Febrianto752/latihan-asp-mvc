using IdentityApp.Models.Domain;
using IdentityApp.Models.Dtos;
using IdentityApp.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;

namespace IdentityApp.Repositories.Implementation;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserAuthenticationService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<StatusDto> LoginAsync(LoginDto model)
    {
        var statusDto = new StatusDto();
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user == null)
        {
            statusDto.StatusCode = 0;
            statusDto.Message = "Invalid username";
            return statusDto;
        }

        if (!await _userManager.CheckPasswordAsync(user, model.Password))
        {
            statusDto.StatusCode = 0;
            statusDto.Message = "Invalid password";
            return statusDto;
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
        if (signInResult.Succeeded)
        {
            //var userRoles = await _userManager.GetRolesAsync(user);
            //var authClaims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.UserName)
            //};

            //foreach (var userRole in userRoles)
            //{
            //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            //}

            statusDto.StatusCode = 1;
            statusDto.Message = "Logged in successfully";
            return statusDto;
        }
        else if (signInResult.IsLockedOut)
        {
            statusDto.StatusCode = 0;
            statusDto.Message = "User locked out";
            return statusDto;
        }
        else
        {
            statusDto.StatusCode = 0;
            statusDto.Message = "Error on loggin in";
            return statusDto;
        }
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<StatusDto> RegistrationAsync(RegistrationDto model)
    {
        var statusDto = new StatusDto();
        var userExists = await _userManager.FindByNameAsync(model.Username);

        if (userExists != null)
        {
            statusDto.StatusCode = 0;
            statusDto.Message = "User already exists";
            return statusDto;
        }

        ApplicationUser user = new ApplicationUser
        {
            SecurityStamp = Guid.NewGuid().ToString(),
            Name = model.Name,
            Email = model.Email,
            UserName = model.Username,
            EmailConfirmed = true,
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            statusDto.StatusCode = 0;
            statusDto.Message = "User creation failed";
            return statusDto;
        }

        // role management
        if (!await _roleManager.RoleExistsAsync(model.Role))
        {
            await _roleManager.CreateAsync(new IdentityRole(model.Role));
        }

        if (await _roleManager.RoleExistsAsync(model.Role))
        {
            await _userManager.AddToRoleAsync(user, model.Role);
        }

        statusDto.StatusCode = 1;
        statusDto.Message = "User has registered successfully";
        return statusDto;
    }
}

