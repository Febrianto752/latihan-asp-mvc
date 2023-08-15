using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models;

[Table("roles")]
public class Role : BaseEntity
{
    [Column("name", TypeName = "nvarchar(20)")]
    public string Name { get; set; }

    public ICollection<AccountRole>? AccountRoles { get; set; }
}

