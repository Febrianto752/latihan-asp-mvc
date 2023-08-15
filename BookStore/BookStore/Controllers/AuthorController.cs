using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers;

[Authorize(Roles = "Admin")]
public class AuthorController : Controller
{
    private readonly BookStoreDbContext _dbContext;
    public AuthorController(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var authors = _dbContext.Authors.ToList();
        return View(authors);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create([FromForm] Author author)
    {
        var newAuthor = new Author
        {
            Name = author.Name,
            Age = author.Age,
        };

        _dbContext.Authors.Add(newAuthor);
        _dbContext.SaveChanges();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Details([FromQuery] Guid guid)
    {
        var author = _dbContext.Authors.Include(a => a.Books).FirstOrDefault(a => a.Guid == guid);

        return View(author);
    }

    [HttpGet]
    public IActionResult Edit([FromQuery] Guid guid)
    {
        var author = _dbContext.Authors.FirstOrDefault(a => a.Guid == guid);

        return View(author);
    }

    [HttpPost]
    public IActionResult Edit([FromForm] Author author)
    {
        var updatedAuthor = _dbContext.Authors.FirstOrDefault(a => a.Guid == author.Guid);
        updatedAuthor.Name = author.Name;
        updatedAuthor.Age = author.Age;
        _dbContext.Authors.Update(updatedAuthor);
        _dbContext.SaveChanges();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete([FromForm] Guid guid)
    {
        var deletedAuthor = _dbContext.Authors.FirstOrDefault(a => a.Guid == guid);

        _dbContext.Authors.Remove(deletedAuthor);
        _dbContext.SaveChanges();

        return RedirectToAction("Index");
    }
}

