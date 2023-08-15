using System.ComponentModel;

namespace MVC.DTOs;

public class UserDto
{
    public string Name { get; set; }

    public List<string>? Hobbies { get; set; }

    [DisplayName("last name")]
    public string? LastName { get; set; }
}

