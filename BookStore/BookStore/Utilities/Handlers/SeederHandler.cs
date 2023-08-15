
using Bogus;
using BookStore.Data;
using BookStore.Models;

namespace BookStore.Utilities.Handlers;

public class SeederHandler
{
    private readonly BookStoreDbContext _dbContext;
    public SeederHandler(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SeedData()
    {
        var transaction = _dbContext.Database.BeginTransaction();

        try
        {
            var author1 = new Author
            {
                Name = "Budi Harjo",
                Age = 22,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            var author2 = new Author
            {
                Name = "Joko Supratman",
                Age = 30,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            var author3 = new Author
            {
                Name = "Dwi Lestari",
                Age = 24,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            _dbContext.Authors.AddRange(new List<Author> { author1, author2, author3 });
            _dbContext.SaveChanges();


            var authorGuidChoices = new List<Guid> { author1.Guid, author2.Guid, author3.Guid };

            var bookSeedFaker = new Faker<Book>("id_ID")
                                .RuleFor(b => b.Title, faker => faker.Name.FirstName())
                                .RuleFor(b => b.Quota, faker => faker.Random.Number(1, 100))
                                .RuleFor(b => b.Price, faker => faker.Finance.Amount(1000, 10000))
                                .RuleFor(b => b.AuthorGuid, faker => faker.PickRandom(authorGuidChoices))
                                .RuleFor(b => b.CreatedDate, _ => DateTime.Now)
                                .RuleFor(b => b.ModifiedDate, _ => DateTime.Now);

            var bookSeeds = bookSeedFaker.Generate(100);

            _dbContext.Books.AddRange(bookSeeds);
            _dbContext.SaveChanges();


            var role1 = new Role
            {
                Name = "Admin",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            var role2 = new Role
            {
                Name = "Member",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            _dbContext.Roles.AddRange(new List<Role> { role1, role2 });
            _dbContext.SaveChanges();

            var account1 = new Account
            {
                Email = "optimusprime200039@gmail.com",
                Password = "admin123",
                Username = "leonardo",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            var account2 = new Account
            {
                Email = "ikxe496@gmail.com",
                Password = "member123",
                Username = "febri",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            _dbContext.Accounts.AddRange(new List<Account> { account1, account2 });
            _dbContext.SaveChanges();

            var accountRole1 = new AccountRole
            {
                AccountGuid = account1.Guid,
                RoleGuid = role1.Guid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            var accountRole2 = new AccountRole
            {
                AccountGuid = account2.Guid,
                RoleGuid = role2.Guid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            _dbContext.AccountRoles.AddRange(new List<AccountRole> { accountRole1, accountRole2 });
            _dbContext.SaveChanges();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
        }



    }

    public void RemoveAllData()
    {
        var transaction = _dbContext.Database.BeginTransaction();

        try
        {
            var accountRoles = _dbContext.AccountRoles;
            _dbContext.RemoveRange(accountRoles);

            var accounts = _dbContext.Accounts;
            _dbContext.Accounts.RemoveRange(accounts);

            var roles = _dbContext.Roles;
            _dbContext.Roles.RemoveRange(roles);

            var books = _dbContext.Books;
            _dbContext.Books.RemoveRange(books);


            var authors = _dbContext.Authors;
            _dbContext.Authors.RemoveRange(authors);

            _dbContext.SaveChanges();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
        }

    }

}

