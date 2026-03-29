namespace LibraryTracker.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;

    // Navigation property — EF Core uses this to represent the one-to-many relationship
    public ICollection<Borrower> Borrowers { get; set; } = new List<Borrower>();
}
