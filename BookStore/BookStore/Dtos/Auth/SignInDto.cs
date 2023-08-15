using System.ComponentModel.DataAnnotations;

namespace BookStore.Dtos.Auth;

public class SignInDto
{
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}

