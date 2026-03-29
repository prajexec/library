using Microsoft.EntityFrameworkCore;
using LibraryTracker.Models;

namespace LibraryTracker.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Borrower> Borrowers => Set<Borrower>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed one admin user (password: password123)
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123")
        });
    }
}
