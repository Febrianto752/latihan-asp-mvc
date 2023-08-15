using System.ComponentModel.DataAnnotations;

namespace IdentityApp.Models.Dtos;

public class RegistrationDto
{
    [Required]
    public string Name { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string Username { get; set; }

    [RegularExpression(@"^(?:(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*)$", ErrorMessage = "minimum length password 6 and must contain 1 uppercase, 1 lowercase, 1 special character ")]
    public string Password { get; set; }

    [Compare("Password")]
    public string PasswordConfirm { get; set; }

    public string? Role { get; set; }
}

