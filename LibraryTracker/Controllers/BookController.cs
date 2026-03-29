using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryTracker.Models;
using LibraryTracker.Services;

namespace LibraryTracker.Controllers;

[Authorize]
public class BookController : Controller
{
    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }

    // GET: /book
    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetAll();
        return View(books);
    }

    // GET: /book/details/1
    public async Task<IActionResult> Details(int id)
    {
        var book = await _bookService.GetById(id);
        if (book == null) return NotFound();
        return View(book);
    }

    // GET: /book/create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /book/create
    [HttpPost]
    public async Task<IActionResult> Create(Book book)
    {
        if (!ModelState.IsValid) return View(book);
        await _bookService.Create(book);
        return RedirectToAction(nameof(Index));
    }

    // GET: /book/edit/1
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookService.GetById(id);
        if (book == null) return NotFound();
        return View(book);
    }

    // POST: /book/edit/1
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Book book)
    {
        if (id != book.Id) return NotFound();
        if (!ModelState.IsValid) return View(book);
        await _bookService.Update(book);
        return RedirectToAction(nameof(Index));
    }

    // POST: /book/delete/1
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _bookService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    // GET: /book/borrow/1
    public async Task<IActionResult> Borrow(int id)
    {
        var book = await _bookService.GetById(id);
        if (book == null) return NotFound();
        ViewBag.Book = book;
        return View(new Borrower { BookId = id });
    }

    // POST: /book/borrow/1
    [HttpPost]
    public async Task<IActionResult> Borrow(Borrower borrower)
    {
        await _bookService.AddBorrower(borrower);
        return RedirectToAction(nameof(Details), new { id = borrower.BookId });
    }

    // POST: /book/return/1
    [HttpPost]
    public async Task<IActionResult> Return(int id, int bookId)
    {
        await _bookService.ReturnBook(id);
        return RedirectToAction(nameof(Details), new { id = bookId });
    }
}
