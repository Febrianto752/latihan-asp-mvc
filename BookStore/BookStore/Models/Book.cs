using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models;

[Table("books")]
public class Book : BaseEntity
{
    [Column("title", TypeName = "nvarchar(50)")]
    public string Title { get; set; }

    [Column("quota")]
    public int Quota { get; set; }

    [Column("price")]
    [Precision(10, 2)]
    public decimal Price { get; set; }


    [Column("author_guid")]
    public Guid AuthorGuid { get; set; }

    public Author? Author { get; set; }

}

