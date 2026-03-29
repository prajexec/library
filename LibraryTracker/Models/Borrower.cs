namespace LibraryTracker.Models;

public class Borrower
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime BorrowedOn { get; set; }

    // Nullable — a book that hasn't been returned yet has no return date
    public DateTime? ReturnedOn { get; set; }

    // Foreign key
    public int BookId { get; set; }

    // Navigation property
    public Book Book { get; set; } = null!;
}
