using Microsoft.EntityFrameworkCore;
using SharedData;

namespace SharedData;



public class AppDbContext : DbContext
{
    private readonly string _connectionString;

    // Default constructor (Used by the Console App and Migrations)
    public AppDbContext()
    {
        _connectionString = "Data Source=store.db";
    }

    // New constructor (Used by MAUI to pass a specific path)
    public AppDbContext(string fullPath)
    {
        _connectionString = $"Data Source={fullPath}";
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}