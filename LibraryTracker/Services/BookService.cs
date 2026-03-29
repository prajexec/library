using Microsoft.EntityFrameworkCore;
using LibraryTracker.Data;
using LibraryTracker.Models;

namespace LibraryTracker.Services;

public class BookService
{
    private readonly AppDbContext _context;

    public BookService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAll()
    {
        return await _context.Books.Include(b => b.Borrowers).ToListAsync();
    }

    public async Task<Book?> GetById(int id)
    {
        return await _context.Books.Include(b => b.Borrowers).FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task Create(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddBorrower(Borrower borrower)
    {
        borrower.BorrowedOn = DateTime.Now;
        _context.Borrowers.Add(borrower);
        await _context.SaveChangesAsync();
    }

    public async Task ReturnBook(int borrowerId)
    {
        var borrower = await _context.Borrowers.FindAsync(borrowerId);
        if (borrower != null)
        {
            borrower.ReturnedOn = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
