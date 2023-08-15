using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {

    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AccountRole>()
            .HasIndex(ar => new
            {
                ar.AccountGuid,
                ar.RoleGuid
            }).IsUnique();

        modelBuilder.Entity<Role>()
            .HasIndex(r => new
            {
                r.Name,
            }).IsUnique();

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books);

        //Accounts - Account Roles (One to Many)
        modelBuilder.Entity<Account>()
            .HasMany(account => account.AccountRoles)
            .WithOne(accountroles => accountroles.Account)
            .HasForeignKey(accountroles => accountroles.AccountGuid);

        //Account Roles - Roles (One to Many)
        modelBuilder.Entity<AccountRole>()
            .HasOne(accountroles => accountroles.Role)
            .WithMany(roles => roles.AccountRoles)
            .HasForeignKey(accountrole => accountrole.RoleGuid);
    }
}

