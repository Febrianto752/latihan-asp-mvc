using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers;



public class BookController : Controller
{
    private readonly BookStoreDbContext _dbContext;
    public BookController(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [Authorize(Roles = "Member, Admin")]
    public IActionResult Index(string? title, string? author, int page = 1)
    {
        Console.WriteLine("value cookies =============================");
        var expirationTime = HttpContext.Request.Cookies;

        foreach (var item in expirationTime)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine(expirationTime);

        var limit = 10;
        var skip = (page - 1) * limit;
        var books = _dbContext.Books.Include(b => b.Author).ToList();

        if (title != null)
        {
            books = books.Where(b => b.Title.ToLower().Contains(title.ToLower())).ToList();
        }

        if (author != null && author != "all")
        {
            books = books.Where(b => b.Author!.Name == author).ToList();
        }

        double calculation = books.Count / (double)limit;

        var pages = (int)Math.Ceiling(calculation);

        books = books.Skip(skip).Take(limit).ToList();

        var authors = _dbContext.Authors.Select(a => a.Name).ToList();
        ViewData["authors"] = authors;
        ViewData["pages"] = pages;

        return View(books);
    }

    [HttpGet]
    public IActionResult Details([FromQuery] Guid guid)
    {
        var book = _dbContext.Books.Include(b => b.Author).FirstOrDefault(b => b.Guid == guid);

        if (book is null)
        {
            ViewData["Error"] = "Book detail is not found!";
            return RedirectToAction("Index");
        }

        return View(book);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        var authors = _dbContext.Authors.ToList();
        ViewData["authors"] = authors;


        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Create([FromForm] Book book)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _dbContext.Books.Add(book);
                _dbContext.SaveChanges();

                TempData["Success"] = "Successfully to created new book";


                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Failed to created new book";
                return RedirectToAction("Index");
            }

        }

        var authors = _dbContext.Authors.ToList();
        ViewData["authors"] = authors;


        return View();

    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit([FromQuery] Guid guid)
    {
        var book = _dbContext.Books.FirstOrDefault(b => b.Guid == guid);

        var authors = _dbContext.Authors.ToList();
        ViewData["authors"] = authors;

        return View(book);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit([FromForm] Book book)
    {
        if (ModelState.IsValid)
        {
            var updatedBook = _dbContext.Books.FirstOrDefault(b => b.Guid == book.Guid);

            if (updatedBook == null)
            {
                TempData["Error"] = "Book is not found!";
                return RedirectToAction("Index");
            }
            updatedBook.Title = book.Title;
            updatedBook.Quota = book.Quota;
            updatedBook.Price = book.Price;
            updatedBook.AuthorGuid = book.AuthorGuid;

            try
            {
                _dbContext.Books.Update(updatedBook);
                _dbContext.SaveChanges();
                TempData["Success"] = "Successfully to updated book";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Failed to updated book";
                return RedirectToAction("Index");
            }
        }

        var authors = _dbContext.Authors.ToList();
        ViewData["authors"] = authors;
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete([FromForm] Guid guid)
    {
        var deletedBook = _dbContext.Books.FirstOrDefault(a => a.Guid == guid);
        if (deletedBook == null)
        {
            TempData["Error"] = "Book is not found";
            return RedirectToAction("Index");
        }

        try
        {
            _dbContext.Remove(deletedBook);
            _dbContext.SaveChanges();

            TempData["Success"] = "Successfully to deleted book";
            return RedirectToAction("Index");
        }
        catch
        {
            TempData["Error"] = "Failed to deleted book";
            return RedirectToAction("Index");
        }
    }

}

