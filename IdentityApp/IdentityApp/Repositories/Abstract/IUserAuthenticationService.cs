using IdentityApp.Models.Dtos;

namespace IdentityApp.Repositories.Abstract;

public interface IUserAuthenticationService
{
    Task<StatusDto> LoginAsync(LoginDto model);

    Task<StatusDto> RegistrationAsync(RegistrationDto model);

    Task LogoutAsync();
}

