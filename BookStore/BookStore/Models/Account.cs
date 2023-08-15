using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models;

[Table("accounts")]
public class Account : BaseEntity
{
    [Column("email", TypeName = "nvarchar(100)")]
    public string Email { get; set; }

    [Column("password", TypeName = "nvarchar(255)")]
    public string Password { get; set; }

    [Column("username", TypeName = "nvarchar(50)")]
    public string Username { get; set; }

    public ICollection<AccountRole>? AccountRoles { get; set; }
}

