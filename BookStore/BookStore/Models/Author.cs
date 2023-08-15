using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models;

[Table("authors")]
public class Author : BaseEntity
{
    [Column("name", TypeName = "nvarchar(50)")]
    public string Name { get; set; }

    [Column("age")]
    public int Age { get; set; }

    public ICollection<Book> Books { get; set; }
}

